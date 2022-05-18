using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly WebMarketDbContext _db;

        public ProductCategoryRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(ProductCategory entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<ProductCategory>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ProductCategory> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
