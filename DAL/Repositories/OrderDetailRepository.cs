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
    public class OrderDetailRepository : IOrderDetailRepository
    {
        private readonly WebMarketDbContext _db;

        public OrderDetailRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            return await _db.OrderDetails.ToListAsync();
        }

        public async Task<OrderDetail> GetByIdAsync(Guid id)
        {
            return await _db.OrderDetails.FirstOrDefaultAsync(od => od.Id == id);
        }

        public Task AddAsync(OrderDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given order details are null");

            return AddInternalAsync(entity);
        }

        private async Task AddInternalAsync(OrderDetail entity)
        {
            var existsInDb = await _db.OrderDetails.AnyAsync(od => od.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Order details with this id already exist", nameof(entity));

            await _db.OrderDetails.AddAsync(entity);
        }

        public void Delete(OrderDetail entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given order details are null");

            var itemToDelete = _db.OrderDetails.FirstOrDefault(od => od.Id == entity.Id);
            if (itemToDelete == null)
                throw new EntityNotFoundException("Order details not found in DB", nameof(entity));

            _db.OrderDetails.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.OrderDetails.FirstOrDefaultAsync(od => od.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Order details with this id not found", nameof(id));

            _db.OrderDetails.Remove(itemToDelete);
        }

        public void Update(OrderDetail entity)
        {
            var existsInDb = _db.OrderDetails.Any(od => od.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Order details not found in DB", nameof(entity));

            _db.OrderDetails.Update(entity);
        }

        public async Task<IEnumerable<OrderDetail>> GetAllWithDetailsAsync()
        {
            return await _db.OrderDetails.Include(od => od.Product)
                .ThenInclude(p => p.Category).Include(od => od.Order)
                .ThenInclude(o => o.Customer).ThenInclude(c => c.Person)
                .ToListAsync();
        }
    }
}
