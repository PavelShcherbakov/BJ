using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperDeckRepository : DapperGenericRepository<Card, Guid>, IDeckRepository
    {
        public DapperDeckRepository(IConfiguration config) : base(config) { }

        public async Task<IEnumerable<Card>> GetCardsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Decks WHERE GameId = @gameId;";
                var result = await conn.QueryAsync<Card>(sql, new { gameId });
                return result;
            }
        }

        public async Task<int> GetCountCardsAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Decks WHERE GameId = @gameId;";
                var result = (await conn.QueryAsync<Card>(sql, new { gameId })).Count();
                return result;
            }
        }

        public async Task<IEnumerable<Card>> GetRandomCardsByGameIdAsync(Guid gameId, int numOfCards)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT TOP (@numOfCards) * FROM Decks ORDER BY newid();";
                conn.Open();
                var result = await conn.QueryAsync<Card>(sql, new { numOfCards });
                return result;
            }
        }
    }
}
