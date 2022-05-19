using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Exceptions;
using DAL.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class ProductCategoryRepository : IProductCategoryRepository
    {
        private readonly WebMarketDbContext _db;

        public ProductCategoryRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<ProductCategory>> GetAllAsync()
        {
            return await _db.ProductCategories.ToListAsync();
        }

        public async Task<ProductCategory> GetByIdAsync(Guid id)
        {
            return await _db.ProductCategories.FirstOrDefaultAsync(pc => pc.Id == id);
        }

        public Task AddAsync(ProductCategory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given product category is null");

            return AddInternalAsync(entity);
        }

        private async Task AddInternalAsync(ProductCategory entity)
        {
            var existsInDb = await _db.ProductCategories.AnyAsync(pc => pc.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Product category with this id already exist", nameof(entity));

            await _db.ProductCategories.AddAsync(entity);
        }

        public void Delete(ProductCategory entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given product category is null");

            var itemToDelete = _db.ProductCategories.FirstOrDefault(pc => pc.Id == entity.Id);
            if (itemToDelete == null)
                throw new EntityNotFoundException("Product category not found in DB", nameof(entity));

            _db.ProductCategories.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.ProductCategories.FirstOrDefaultAsync(pc => pc.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Product category with this id not found", nameof(id));

            _db.ProductCategories.Remove(itemToDelete);
        }

        public void Update(ProductCategory entity)
        {
            var existsInDb = _db.ProductCategories.Any(pc => pc.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Product category not found in DB", nameof(entity));

            _db.ProductCategories.Update(entity);
        }

        public async Task<IEnumerable<ProductCategory>> GetAllWithDetailsAsync()
        {
            return await _db.ProductCategories.Include(pc => pc.Products)
                .ThenInclude(p => p.OrderDetails).ThenInclude(od => od.Order)
                .ThenInclude(o => o.Customer).ThenInclude(c => c.Person)
                .ToListAsync();
        }

        public async Task<ProductCategory> GetByIdWithDetailsAsync(Guid id)
        {
            return await _db.ProductCategories.Include(pc => pc.Products)
                .ThenInclude(p => p.OrderDetails).ThenInclude(od => od.Order)
                .ThenInclude(o => o.Customer).ThenInclude(c => c.Person)
                .FirstOrDefaultAsync(pc => pc.Id == id);
        }
    }
}
