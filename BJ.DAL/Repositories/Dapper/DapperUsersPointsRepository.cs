using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Data;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperUsersPointsRepository : DapperGenericRepository<UsersPoints, Guid>, IUsersPointsRepository
    {
        public DapperUsersPointsRepository(IConfiguration config) : base(config) { }

        public async Task<UsersPoints> GetPointsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM UsersPoints WHERE GameId = @gameId;";
                var result = await conn.QuerySingleOrDefaultAsync<UsersPoints>(sql, new { gameId });
                return result;
            }
        }
    }
}
