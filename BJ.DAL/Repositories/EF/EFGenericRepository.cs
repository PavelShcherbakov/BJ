using BJ.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFGenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private ApplicationDbContext _context;
        protected DbSet<TEntity> _dbSet;

        public EFGenericRepository(ApplicationDbContext context)
        {
            _context = context;
            _dbSet = context.Set<TEntity>();
        }

        public async Task CreateAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
            await _context.SaveChangesAsync();
        }

        //public virtual IEnumerable<TEntity> Find(Func<TEntity, bool> predicate)
        //{
        //    var result = _dbSet.Where(predicate).ToList();
        //    return result;
        //}

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            var result = await _dbSet.ToListAsync();
            return result;
        }

        public IEnumerable<TEntity> GetAll()
        {
            var result =  _dbSet.ToList();
            return result;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            DeleteTrackedEntities();
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            DeleteTrackedEntities();
            _dbSet.UpdateRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveAsync(TEntity entity)
        {
            DeleteTrackedEntities();
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            DeleteTrackedEntities();
            _dbSet.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            var result = await _dbSet.FindAsync(id);
            return result;
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
             _dbSet.AddRange(entities);
             _context.SaveChanges();
        }

        private void DeleteTrackedEntities()
        {
            var changedEntriesCopy = _context.ChangeTracker.Entries().ToList();
            foreach (var entity in changedEntriesCopy)
            {
                _context.Entry(entity.Entity).State = EntityState.Detached;
            }
        }

        public async Task<int> GetTotalCount()
        {
            var result = await _dbSet.CountAsync();
            return result;
        }
    }
}
