using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IRepository<T,TId> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        IEnumerable<T> GetAll();
        int GetTotalCount();
        Task<T> GetByIdAsync(TId id);    
        Task CreateAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void AddRange(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task UpdateRangeAsync(IEnumerable<T> entities);
        Task RemoveAsync(T entity);
        Task RemoveRangeAsync(IEnumerable<T> entities);
    }
}
