using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class CustomerRepository:ICustomerRepository
    {
        private readonly WebMarketDbContext _db;

        public CustomerRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<Customer>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Customer entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Customer entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Customer>> GetAllWithDetailsAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Customer> GetByIdWithDetailsAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
