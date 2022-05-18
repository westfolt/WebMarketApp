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
        Task<int> AddCategoryAsync(ProductCategoryDto entity);
        Task<bool> UpdateCategoryAsync(ProductCategoryDto entity);
        Task<bool> DeleteCategoryAsync(ProductCategoryDto entity);
    }
}
