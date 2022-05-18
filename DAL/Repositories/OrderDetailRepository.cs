using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class OrderDetailRepository:IOrderDetailRepository
    {
        private readonly WebMarketDbContext _db;

        public OrderDetailRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<OrderDetail>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderDetail> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(OrderDetail entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<OrderDetail>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
