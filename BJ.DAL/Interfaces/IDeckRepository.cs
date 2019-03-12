using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IDeckRepository : IRepository<Card, Guid>
    {
        Task<IEnumerable<Card>> GetRandomCardsByGameIdAsync(Guid gameId, int numOfCards);
        Task<int> GetCountCardsAsync(Guid gameId);
        Task<IEnumerable<Card>> GetCardsByGameIdAsync(Guid gameId);
    }
}
