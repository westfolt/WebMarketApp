using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;

namespace BLL.Services
{
    public class OrderService:IOrderService
    {
        public Task<IEnumerable<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(OrderDto entity)
        {
            throw new NotImplementedException();
        }

        public Task AddProductAsync(Guid productId, Guid orderId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteProductAsync(Guid productId, Guid orderId, int quantity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDetailDto>> GetOrderDetailsAsync(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> TotalToPay(Guid orderId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> ChangeOrderStatus(Guid orderId, OrderStatusDto newStatus)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDto>> GetOrdersForPeriodAsync(DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
