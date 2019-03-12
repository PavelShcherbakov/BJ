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
    public class DapperUsersStepRepository : DapperGenericRepository<UsersStep, Guid>, IUsersStepRepository
    {
        public DapperUsersStepRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<UsersStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM UsersSteps WHERE GameId = @gameId;";
                var result = await conn.QueryAsync<UsersStep>(sql, new { gameId });
                return result;
            }
        }
    }
}
