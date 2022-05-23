using BLL.Dto;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICustomerService : ICrud<CustomerDto>
    {
        Task<IEnumerable<CustomerDto>> GetCustomersByProductIdAsync(Guid productId);
    }
}
