using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class OrderRepository:IOrderRepository
    {
        private readonly WebMarketDbContext _db;

        public OrderRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<Order>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Order entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Order entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Order>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Order> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
