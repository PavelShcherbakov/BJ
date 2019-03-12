using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperBotsStepRepository : DapperGenericRepository<BotsStep, Guid>, IBotsStepRepository
    {
        public DapperBotsStepRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<BotsStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            string sql = @"SELECT 
                                bs.Id, bs.CreationDate, bs.StepNumder, bs.GameId, bs.BotId, bs.Rank, bs.Suit, 
                                b.Id, b.CreationDate, b.Name  
                            FROM BotsSteps AS bs LEFT JOIN Bots AS b ON bs.BotId = b.Id 
                            WHERE bs.GameId=@gameId";

            using (var conn = Connection)
            {
                var result = await conn.QueryAsync<BotsStep, Bot, BotsStep>(
                        sql,
                        (bs, bot) =>
                        {
                            bs.Bot = bot;
                            return bs;
                        },
                        param: new { gameId });
                return result;
            }
        }
    }
}
