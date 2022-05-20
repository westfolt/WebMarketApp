using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Interfaces
{
    public interface ICrud<TDto> where TDto : class
    {
        Task<IEnumerable<TDto>> GetAllAsync();
        Task<TDto> GetByIdAsync(Guid id);
        Task<Guid> AddAsync(TDto entity);
        Task UpdateAsync(TDto entity);
        Task DeleteAsync(Guid entityId);
    }
}
