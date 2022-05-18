using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class ProductRepository:IProductRepository
    {
        private readonly WebMarketDbContext _db;

        public ProductRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<Product>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Product entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Product entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Product>> GetAllWithDetailAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Product> GetByIdWithDetailAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
