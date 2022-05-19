using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;
using System.Linq;
using DAL.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly WebMarketDbContext _db;

        public CustomerRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Customer>> GetAllAsync()
        {
            return await _db.Customers.ToListAsync();
        }

        public async Task<Customer> GetByIdAsync(Guid id)
        {
            return await _db.Customers.FirstOrDefaultAsync(c=>c.Id == id);
        }

        public Task AddAsync(Customer entity)
        {
            if(entity == null)
                throw new ArgumentNullException(nameof(entity), "Given customer is null");

            return AddInternalAsync(entity);
        }

        private async Task AddInternalAsync(Customer entity)
        {
            var existsInDb = await _db.Customers.AnyAsync(c=>c.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Customer with this id already exists", nameof(entity));

            await _db.Customers.AddAsync(entity);
        }

        public void Delete(Customer entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given customer is null");

            var itemToDelete = _db.Customers.FirstOrDefault(c=>c.Id == entity.Id);
            if (itemToDelete == null)
                throw new EntityNotFoundException("Customer not found in DB", nameof(entity));

            _db.Customers.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.Customers.FirstOrDefaultAsync(c=>c.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Customer with this id not found", nameof(id));

            _db.Customers.Remove(itemToDelete);
        }

        public void Update(Customer entity)
        {
            var existsInDb = _db.Customers.Any(c => c.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Customer not found in DB", nameof(entity));

            _db.Customers.Update(entity);
        }

        public async Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            return await _db.Customers.Include(c => c.Person).Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails).ThenInclude(od => od.Product).ThenInclude(p => p.Category)
                .ToListAsync();
        }

        public async Task<Customer> GetByIdWithDetailsAsync(Guid id)
        {
            return await _db.Customers.Include(c => c.Person).Include(c => c.Orders)
                .ThenInclude(o => o.OrderDetails).ThenInclude(od => od.Product)
                .ThenInclude(p => p.Category).FirstOrDefaultAsync(c => c.Id == id);
        }
    }
}
