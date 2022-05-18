using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Interfaces
{
    public interface IUnitOfWork
    {
        ICustomerRepository CustomerRepository { get; }
        IPersonRepository PersonRepository { get; }
        IProductRepository ProductRepository { get; }
        IProductCategoryRepository ProductCategoryRepository { get; }
        IOrderRepository OrderRepository { get; }
        IOrderDetailRepository OrderDetailRepository { get; }
        Task SaveAsync();
    }
}
