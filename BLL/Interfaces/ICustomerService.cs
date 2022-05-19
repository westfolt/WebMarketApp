using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;

namespace BLL.Interfaces
{
    public interface ICustomerService : ICrud<CustomerDto>
    {
        Task<IEnumerable<CustomerDto>> GetCustomersByProductAsync(Guid productId);
    }
}
