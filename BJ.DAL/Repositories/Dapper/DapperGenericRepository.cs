using BJ.DAL.Interfaces;
using Dapper.Contrib.Extensions;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperGenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        private readonly IConfiguration _config;

        public DapperGenericRepository(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                var connectString = _config.GetConnectionString("DefaultConnection");
                return new SqlConnection(connectString);
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Insert(entities);
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                await conn.InsertAsync(entities);
            }
        }

        public async Task CreateAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.InsertAsync(entity);
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                var result = conn.GetAll<TEntity>();
                return result;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.GetAllAsync<TEntity>();
                return result;
            }
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            using (IDbConnection conn = Connection)
            {
                var result = await conn.GetAsync<TEntity>(id);
                return result;
            }
        }

        public async Task RemoveAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                await conn.DeleteAsync(entity);
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                await conn.DeleteAsync(entities);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                await conn.UpdateAsync(entity);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                await conn.UpdateAsync(entities);
            }
        }

        public async Task<int> GetTotalCount()
        {
            using (IDbConnection conn = Connection)
            {
                var result = (await conn.GetAllAsync<TEntity>()).Count();
                return result;
            }
        }
    }
}
