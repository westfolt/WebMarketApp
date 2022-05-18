using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync();
        Task<int> AddAsync(TDto entity);
        Task<bool> UpdateAsync(TDto entity);
        Task<bool> DeleteAsync(TDto entity);
    }
}
