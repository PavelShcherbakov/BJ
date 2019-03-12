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
    public class DapperBotRepository : DapperGenericRepository<Bot, Guid>, IBotRepository
    {
        public DapperBotRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<Bot>> GetRandomBotsAsync(int numOfBots)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT TOP (@numOfBots) * FROM Bots ORDER BY newid();";
                conn.Open();
                var result = await conn.QueryAsync<Bot>(sql, new { numOfBots });
                return result;
            }
        }
    }

}
