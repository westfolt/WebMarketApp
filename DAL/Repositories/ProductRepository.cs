using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DAL.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly WebMarketDbContext _db;

        public ProductRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Product>> GetAllAsync()
        {
            return await _db.Products.ToListAsync();
        }

        public async Task<Product> GetByIdAsync(Guid id)
        {
            return await _db.Products.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task AddAsync(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given product is null");

            return AddAsyncInternal(entity);
        }

        private async Task AddAsyncInternal(Product entity)
        {
            var existsInDb = await _db.Products.AnyAsync(p => p.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Product with this id already exists", nameof(entity));

            await _db.Products.AddAsync(entity);
        }

        public void Delete(Product entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given product is null");

            var itemToDelete = _db.Products.FirstOrDefault(p => p.Id == entity.Id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Product not found in DB", nameof(entity));

            _db.Products.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.Products.FirstOrDefaultAsync(p => p.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Product with this id not found", nameof(id));

            _db.Products.Remove(itemToDelete);
        }

        public void Update(Product entity)
        {
            var existsInDb = _db.Products.Any(p => p.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Product not found in DB", nameof(entity));

            _db.Products.Update(entity);
        }

        public async Task<IEnumerable<Product>> GetAllWithDetailsAsync()
        {
            return await _db.Products.Include(p => p.Category).Include(p => p.OrderDetails)
                .ThenInclude(od => od.Order).ThenInclude(o => o.Customer)
                .ThenInclude(c => c.Person).ToListAsync();
        }

        public async Task<Product> GetByIdWithDetailsAsync(Guid id)
        {
            return await _db.Products.Include(p => p.Category).Include(p => p.OrderDetails)
                .ThenInclude(od => od.Order).ThenInclude(o => o.Customer)
                .ThenInclude(c => c.Person).FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
