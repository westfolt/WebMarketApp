using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DAL.Data;
using DAL.Entities;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public class PersonRepository:IPersonRepository
    {
        private readonly WebMarketDbContext _db;

        public PersonRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public Task<IEnumerable<Person>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Person> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task AddAsync(Person entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(Person entity)
        {
            throw new NotImplementedException();
        }

        public Task DeleteByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Person entity)
        {
            throw new NotImplementedException();
        }
    }
}
