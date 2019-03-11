using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.Extensions.Configuration;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperDeckRepository : DapperGenericRepository<Card, Guid>, IDeckRepository
    {
        public DapperDeckRepository(IConfiguration config) : base(config)
        {
        }

        public Task<IEnumerable<Card>> GetCardsByGameIdAsync(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<int> GetCountCardsAsync(Guid gameId)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Card>> GetRandomCardsByGameId(Guid gameId, int numOfCards)
        {
            throw new NotImplementedException();
        }
    }
}
