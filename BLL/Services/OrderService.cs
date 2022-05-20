using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Exceptions;
using BLL.Interfaces;
using BLL.Validation;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class OrderService:IOrderService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public OrderService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            try
            {
                var takenFromDb = await _unitOfWork.OrderRepository.GetAllWithDetailsAsync();
                var mapped = _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<OrderDto> GetByIdAsync(Guid id)
        {
            try
            {
                var takenFromDb = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(id);
                var mapped = _mapper.Map<Order, OrderDto>(takenFromDb);
                return mapped;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<Guid> AddAsync(OrderDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
            {
                throw new WebMarketException(validationErrors, nameof(entity));
            }

            try
            {
                var mapped = _mapper.Map<OrderDto, Order>(entity);
                await _unitOfWork.OrderRepository.AddAsync(mapped);
                await _unitOfWork.SaveAsync();
                return mapped.Id;
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task UpdateAsync(OrderDto entity)
        {
            if (!DtoValidationHelper.TryValidate(entity, out var validationErrors))
            {
                throw new WebMarketException(validationErrors, nameof(entity));
            }

            try
            {
                var mapped = _mapper.Map<OrderDto, Order>(entity);
                _unitOfWork.OrderRepository.Update(mapped);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task DeleteAsync(Guid entityId)
        {
            try
            {
                await _unitOfWork.OrderRepository.DeleteByIdAsync(entityId);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task AddProductAsync(Guid productId, Guid orderId, int quantity)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            if (order == null)
                throw new WebMarketException("Order for update does not exist");

            //if such product already exists in users order
            if (order.OrderDetails != null && order.OrderDetails.Any(od => od.ProductId == productId))
            {
                //if user tries to add negative value greater than products count in cart
                if (order.OrderDetails.First(od => od.ProductId == productId).Quantity + quantity < 0)
                    throw new ArgumentException("Trying to remove too many products", nameof(quantity));

                await AddProductToExistingInternalAsync(productId, order, quantity);
            }
            else
            {
                var product = await _unitOfWork.ProductRepository.GetByIdAsync(productId);
                if (product == null)
                    throw new WebMarketException("Product you trying to add doesn't exist in storage",
                        nameof(productId));

                await AddProductNewInternalAsync(productId, order, quantity);
            }
        }

        private async Task AddProductToExistingInternalAsync(Guid productId, Order order, int quantity)
        {
            try
            {
                var existingOrderDetail = order.OrderDetails.First(od => od.ProductId == productId);
                existingOrderDetail.Quantity += quantity;

                if (existingOrderDetail.Quantity == 0)
                    _unitOfWork.OrderDetailRepository.Delete(existingOrderDetail);
                else
                    _unitOfWork.OrderDetailRepository.Update(existingOrderDetail);
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }

            await _unitOfWork.SaveAsync();
        }

        private async Task AddProductNewInternalAsync(Guid productId, Order order, int quantity)
        {
            try
            {
                var product = await _unitOfWork.ProductRepository.GetByIdWithDetailsAsync(productId);
                OrderDetail newDetail = new OrderDetail()
                {
                    DiscountUnitPrice = product.Price - (product.Price * order.Customer.DiscountPercent) / 100,
                    UnitPrice = product.Price,
                    Quantity = quantity,
                    Order = order,
                    OrderId = order.Id,
                    Product = product,
                    ProductId = product.Id
                };

                order.OrderDetails ??= new List<OrderDetail>();
                order.OrderDetails.Add(newDetail);
                await _unitOfWork.OrderDetailRepository.AddAsync(newDetail);
                _unitOfWork.OrderRepository.Update(order);

                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task DeleteProductAsync(Guid productId, Guid orderId, int quantity)
        {
            var order = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            if(order == null)
                throw new WebMarketException("Order for update does not exist");

            var orderDetail = order.OrderDetails.FirstOrDefault(od=>od.ProductId == productId);
            if (orderDetail == null)
                throw new WebMarketException("Order does not contain details with this product", nameof(productId));
            orderDetail.Quantity -= quantity;
            if(orderDetail.Quantity == 0)
                _unitOfWork.OrderDetailRepository.Delete(orderDetail);
            else if (orderDetail.Quantity < 0)
                throw new WebMarketException("Trying to remove too much products", nameof(quantity));
            else
                _unitOfWork.OrderDetailRepository.Update(orderDetail);

            try
            {
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync(Guid orderId)
        {
            var takenFromDb = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            if (takenFromDb == null)
                throw new WebMarketException("Order with this id not found", nameof(orderId));

            if (takenFromDb.OrderDetails == null)
                throw new WebMarketException("Order does not contain any details", nameof(orderId));

            return _mapper.Map<IEnumerable<OrderDetail>, IEnumerable<OrderDetailDto>>(takenFromDb.OrderDetails);
        }

        public async Task<decimal> TotalToPay(Guid orderId)
        {
            var takenFromDb = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            decimal toPay = 0;
            if(takenFromDb == null)
                throw new WebMarketException("Order with this id not found", nameof(orderId));

            if (takenFromDb.OrderDetails == null)
                throw new WebMarketException("Order does not contain any details", nameof(orderId));

            foreach (var detail in takenFromDb.OrderDetails)
            {
                toPay += detail.Quantity * detail.DiscountUnitPrice;
            }

            return toPay;
        }

        public async Task ChangeOrderStatus(Guid orderId, OrderStatusDto newStatus)
        {
            var takenFromDb = await _unitOfWork.OrderRepository.GetByIdWithDetailsAsync(orderId);
            if (takenFromDb == null)
                throw new WebMarketException("Order with this id not found", nameof(orderId));

            try
            {
                var mapped = _mapper.Map<Order, OrderDto>(takenFromDb);
                mapped.OrderStatus = newStatus;
                takenFromDb = _mapper.Map<OrderDto, Order>(mapped);
                _unitOfWork.OrderRepository.Update(takenFromDb);
                await _unitOfWork.SaveAsync();
            }
            catch (Exception ex)
            {
                throw new WebMarketException(ex.Message);
            }
        }

        public async Task<IEnumerable<OrderDto>> GetOrdersForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            var takenFromDb = await _unitOfWork.OrderRepository.GetAllWithDetailsAsync();
            if (takenFromDb == null)
                throw new WebMarketException("No orders in DB");

            var filtered = takenFromDb.Where(o => o.OperationDate >= startDate && o.OperationDate <= endDate);
            return _mapper.Map<IEnumerable<Order>, IEnumerable<OrderDto>>(filtered);
        }
    }
}
