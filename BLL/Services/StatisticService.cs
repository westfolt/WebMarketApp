using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Dto;
using BLL.Interfaces;
using DAL.Entities;
using DAL.Interfaces;

namespace BLL.Services
{
    public class StatisticService:IStatisticService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public StatisticService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ProductDto>> GetMostPopularProductsAsync(int productCount)
        {
            var takenFromDb = (await _unitOfWork.OrderDetailRepository.GetAllWithDetailsAsync())
                .Select(od => new { od.Product, od.Quantity }).GroupBy(n => n.Product)
                .Select(n => new { Product = n.Key, TotalQuantity = n.Sum(m => m.Quantity) })
                .OrderByDescending(n => n.TotalQuantity)
                .Take(productCount).Select(p => p.Product);

            var mapped = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(takenFromDb);
            return mapped;
        }

        public async Task<IEnumerable<ProductDto>> GetMostPopularProductsForCustomerAsync(int productCount, Guid customerId)
        {
            var takenFromDb = (await _unitOfWork.OrderRepository.GetAllWithDetailsAsync())
                .Where(o => o.CustomerId == customerId)
                .SelectMany(o => o.OrderDetails, (o, od) => new { od.Product, od.Quantity }).GroupBy(n => n.Product)
                .Select(n => new { Product = n.Key, TotalQuantity = n.Sum(m => m.Quantity) })
                .OrderByDescending(n => n.TotalQuantity)
                .Take(productCount).Select(p => p.Product);

            var mapped = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductDto>>(takenFromDb);

            return mapped;
        }

        public async Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.OrderRepository.GetAllWithDetailsAsync())
                .Where(o => o.OperationDate >= startDate && o.OperationDate <= endDate)
                .SelectMany(o => o.OrderDetails,
                    (o, od) => new { o.Customer, Payed = od.DiscountUnitPrice * od.Quantity })
                .GroupBy(n => n.Customer)
                .Select(n => new CustomerActivityDto()
                    { CustomerId = n.Key.Id, CustomerName = $"{n.Key.Person.Name} {n.Key.Person.Surname}", OrderSum = n.Sum(m => m.Payed) })
                .OrderByDescending(n => n.OrderSum)
                .Take(customerCount);
        }

        public async Task<decimal> GetIncomeOfCategoryInPeriod(Guid categoryId, DateTime startDate, DateTime endDate)
        {
            return (await _unitOfWork.OrderRepository.GetAllWithDetailsAsync())
                .Where(o => o.OperationDate >= startDate && o.OperationDate <= endDate)
                .SelectMany(o => o.OrderDetails,
                    (o, od) => new { od.Product.ProductCategoryId, MoneySum = od.DiscountUnitPrice * od.Quantity })
                .Where(n => n.ProductCategoryId == categoryId).Sum(n => n.MoneySum);
        }
    }
}
