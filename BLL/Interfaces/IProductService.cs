using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    internal interface IProductService :ICrud<ProductDto>
    {
        Task<IEnumerable<ProductDto>> GetByFilterAsync(FilterSearchDto filterSearch);
        Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync();
        Task<Guid> AddCategoryAsync(ProductCategoryDto entity);
        Task UpdateCategoryAsync(ProductCategoryDto entity);
        Task DeleteCategoryAsync(Guid entityId);
    }
}
