using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IStatisticService
    {
        Task<IEnumerable<ProductDto>> GetMostPopularProductsAsync(int productCount);
        Task<IEnumerable<ProductDto>> GetMostPopularProductsForCustomerAsync(int productCount, Guid customerId);

        Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(int customerCount, DateTime startDate,
            DateTime endDate);
        Task<decimal> GetIncomeOfCategoryInPeriod(Guid categoryId, DateTime startDate, DateTime endDate);
    }
}
