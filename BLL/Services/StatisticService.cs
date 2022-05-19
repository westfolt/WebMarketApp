using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;

namespace BLL.Services
{
    public class StatisticService:IStatisticService
    {
        public Task<IEnumerable<ProductDto>> GetMostPopularProductsAsync(int productCount)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetMostPopularProductsForCustomerAsync(int productCount, Guid customerId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(Guid customerCount, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public Task<decimal> GetIncomeOfCategoryInPeriod(Guid categoryId, DateTime startDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }
    }
}
