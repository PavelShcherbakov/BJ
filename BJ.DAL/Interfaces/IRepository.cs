using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IRepository<T,TId> where T : class
    {
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(TId id);
        IEnumerable<T> Find(Func<T, Boolean> predicate);
        Task CreateAsync(T item);
        Task AddRangeAsync(IEnumerable<T> collection);
        void AddRange(IEnumerable<T> collection);
        Task UpdateAsync(T item);
        Task RemoveAsync(T item);
        int Count(Func<T, bool> predicate);
    }
}
