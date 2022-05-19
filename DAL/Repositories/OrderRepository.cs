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
    public class OrderRepository:IOrderRepository
    {
        private readonly WebMarketDbContext _db;

        public OrderRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Order>> GetAllAsync()
        {
            return await _db.Orders.ToListAsync();
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);
        }

        public Task AddAsync(Order entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), "Given order is null");

            return AddInternalAsync(entity);
        }

        private async Task AddInternalAsync(Order entity)
        {
            var existsInDb = await _db.Orders.AnyAsync(o => o.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Order with this id already exists", nameof(entity));

            await _db.Orders.AddAsync(entity);
        }

        public void Delete(Order entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given order is null");

            var itemToDelete = _db.Orders.FirstOrDefault(o => o.Id == entity.Id);
            if (itemToDelete == null)
                throw new EntityNotFoundException("Order not found in DB", nameof(entity));

            _db.Orders.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.Orders.FirstOrDefaultAsync(o => o.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Order with this id not found", nameof(id));

            _db.Orders.Remove(itemToDelete);
        }

        public void Update(Order entity)
        {
            var existsInDb = _db.Orders.Any(o => o.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Order not found in DB", nameof(entity));

            _db.Orders.Update(entity);
        }

        public async Task<IEnumerable<Order>> GetAllWithDetailsAsync()
        {
            return await _db.Orders.Include(o => o.Customer).ThenInclude(c => c.Person)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category).ToListAsync();
        }

        public async Task<Order> GetByIdWithDetailsAsync(Guid id)
        {
            return await _db.Orders.Include(o => o.Customer).ThenInclude(c => c.Person)
                .Include(o => o.OrderDetails).ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category).FirstOrDefaultAsync(o => o.Id == id);
        }
    }
}
