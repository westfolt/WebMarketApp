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
    public class PersonRepository : IPersonRepository
    {
        private readonly WebMarketDbContext _db;

        public PersonRepository(WebMarketDbContext context)
        {
            _db = context;
        }

        public async Task<IEnumerable<Person>> GetAllAsync()
        {
            return await _db.Persons.ToListAsync();
        }

        public async Task<Person> GetByIdAsync(Guid id)
        {
            return await _db.Persons.FirstOrDefaultAsync(p => p.Id == id);
        }

        public Task AddAsync(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given person is null");

            return AddInternalAsync(entity);
        }

        private async Task AddInternalAsync(Person entity)
        {
            var existsInDb = await _db.Persons.AnyAsync(p => p.Id == entity.Id);

            if (existsInDb)
                throw new EntityAreadyExistsException("Person with this id already exists", nameof(entity));

            await _db.Persons.AddAsync(entity);
        }

        public void Delete(Person entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity), "Given person is null");

            var itemToDelete = _db.Persons.FirstOrDefault(p => p.Id == entity.Id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Person not found in DB", nameof(entity));

            _db.Persons.Remove(itemToDelete);
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var itemToDelete = await _db.Persons.FirstOrDefaultAsync(p => p.Id == id);

            if (itemToDelete == null)
                throw new EntityNotFoundException("Person with this id not found", nameof(id));

            _db.Persons.Remove(itemToDelete);
        }

        public void Update(Person entity)
        {
            var existsInDb = _db.Persons.Any(p => p.Id == entity.Id);

            if (!existsInDb)
                throw new EntityNotFoundException("Person not found in DB", nameof(entity));

            _db.Persons.Update(entity);
        }
    }
}
