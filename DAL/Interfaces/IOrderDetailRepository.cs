using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.Entities;

namespace DAL.Interfaces
{
    public interface IOrderDetailRepository:IRepository<OrderDetail>
    {
        Task<IEnumerable<OrderDetail>> GetAllWithDetailsAsync();
    }
}
