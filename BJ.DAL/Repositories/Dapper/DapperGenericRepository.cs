using BJ.DAL.Interfaces;
using DapperExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperGenericRepository<TEntity, TId> : IRepository<TEntity, TId> where TEntity : class
    {
        //private readonly IConfiguration _config;

        //public DapperGenericRepository(IConfiguration config)
        //{
        //    _config = config;
        //}

        //public IDbConnection Connection
        //{
        //    get
        //    {
        //        var connectString = _config.GetConnectionString("DefaultConnection");
        //        return new SqlConnection(connectString);
        //    }
        //}

        private readonly string _connectionString;
        private IDbConnection _connection;

        public DapperGenericRepository(IConfiguration config)
        {
            _connectionString = config.GetConnectionString("DefaultConnection");
        }

        public IDbConnection Connection
        {
            get
            {
                if (_connection == null)
                {
                    _connection = new SqlConnection(_connectionString);
                }

                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }

                return _connection;
            }
        }

        public void Dispose()
        {
            if (_connection != null && _connection.State == ConnectionState.Open)
            {
                _connection.Close();
                _connection = null;
            }
        }

        public void AddRange(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                conn.Insert(entities);
            }
        }

        public async Task AddRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.InsertAsync(entities);
            }
        }

        public Task CreateAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = conn.Insert(entity);
                return result;
            }
        }

        public IEnumerable<TEntity> GetAll()
        {
            using (IDbConnection conn = Connection)
            {
                //conn.Open();
                var result = conn.GetList<TEntity>();
                return result;
            }
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var result = await conn.GetListAsync<TEntity>();
                return result;
            }
        }

        public async Task<TEntity> GetByIdAsync(TId id)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                var entity = await conn.GetAsync<TEntity>(id);
            }
            return null;
        }

        public async Task RemoveAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.DeleteAsync(entity);
            }
        }

        public async Task RemoveRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.DeleteAsync(entities);
            }
        }

        public async Task UpdateAsync(TEntity entity)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.UpdateAsync(entity);
            }
        }

        public async Task UpdateRangeAsync(IEnumerable<TEntity> entities)
        {
            using (IDbConnection conn = Connection)
            {
                conn.Open();
                await conn.UpdateAsync(entities);
            }
        }

        public async Task<int> GetTotalCount()
        {



            using (IDbConnection conn = Connection)
            {
                //conn.Open();
                var result = await conn.CountAsync<TEntity>();
                return result;
            }
        }
    }
}
