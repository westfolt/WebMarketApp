using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;

namespace BLL.Services
{
    public class ProductService:IProductService
    {
        public Task<IEnumerable<ProductDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(ProductDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductDto>> GetByFilterAsync(FilterSearchDto filterSearch)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategoryDto>> GetAllProductCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<int> AddCategoryAsync(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateCategoryAsync(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteCategoryAsync(ProductCategoryDto entity)
        {
            throw new NotImplementedException();
        }
    }
}
