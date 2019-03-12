using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperGameRepository : DapperGenericRepository<Game, Guid>, IGameRepository
    {
        public DapperGameRepository(IConfiguration config) : base(config) { }

        public async Task<Game> GetActiveGameAsync(string userId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Games WHERE State=0 and UserId = @UserId;";
                conn.Open();
                var result = await conn.QueryFirstOrDefaultAsync<Game>(sql, new { UserId = userId });
                return result;
            }
        }

        public async Task<IEnumerable<Game>> GetСompletedGamesAsync(string userId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Games WHERE State!=0 and UserId = @UserId;";
                conn.Open();
                var result = await conn.QueryAsync<Game>(sql, new { UserId = userId });
                return result;
            }
        }

    }
}
