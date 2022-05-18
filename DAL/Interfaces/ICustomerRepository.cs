using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface ICustomerRepository:IRepository<Customer>
    {
        Task<IEnumerable<Customer>> GetAllWithDetailsAsync();
        Task<Customer> GetByIdWithDetailsAsync(Guid id);
    }
}
