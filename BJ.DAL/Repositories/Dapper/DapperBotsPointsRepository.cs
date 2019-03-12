using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperBotsPointsRepository : DapperGenericRepository<BotsPoints, Guid>, IBotsPointsRepository
    {
        public DapperBotsPointsRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<BotsPoints>> GetPointsByGameIdAsync(Guid gameId)
        {
            string sql = @"SELECT 
                                bp.Id,bp.CreationDate,bp.GameId,bp.BotId,bp.Points,bp.CardsInHand,
                                b.Id,b.CreationDate,b.Name 
                            FROM BotsPoints AS bp LEFT JOIN Bots AS b ON bp.BotId = b.Id
                            WHERE bp.GameId=@gameId";

            using (var conn = Connection)
            {
                var result = await conn.QueryAsync<BotsPoints, Bot, BotsPoints>(
                        sql,
                        (bp, bot) =>
                        {
                            bp.Bot = bot;
                            return bp;
                        },
                        param: new { gameId });
                return result;
            }
        }
    }
}
