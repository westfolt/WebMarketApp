using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BLL.Dto;
using BLL.Interfaces;

namespace BLL.Services
{
    public class CustomerService:ICustomerService
    {
        public Task<IEnumerable<CustomerDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<CustomerDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<int> AddAsync(CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<bool> DeleteAsync(CustomerDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<CustomerDto>> GetCustomersByProductAsync(Guid productId)
        {
            throw new NotImplementedException();
        }
    }
}
