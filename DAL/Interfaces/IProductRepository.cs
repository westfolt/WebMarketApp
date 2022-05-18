using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IProductRepository:IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithDetailAsync();
        Task<Product> GetByIdWithDetailAsync(Guid id);
    }
}
