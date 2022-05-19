using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface IStatisticService
    {
        Task<IEnumerable<ProductDto>> GetMostPopularProductsAsync(int productCount);
        Task<IEnumerable<ProductDto>> GetMostPopularProductsForCustomerAsync(int productCount, Guid customerId);

        Task<IEnumerable<CustomerActivityDto>> GetMostValuableCustomersAsync(Guid customerCount, DateTime startDate,
            DateTime endDate);
        Task<decimal> GetIncomeOfCategoryInPeriod(Guid categoryId, DateTime startDate, DateTime endDate);
    }
}
