using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface IProductService : ICrud<ProductDto>
    {
        Task<IEnumerable<ProductDto>> GetByFilterAsync(FilterSearchDto filterSearch);
        Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync();
        Task<Guid> AddCategoryAsync(ProductCategoryDto entity);
        Task UpdateCategoryAsync(ProductCategoryDto entity);
        Task DeleteCategoryAsync(Guid entityId);
    }
}
