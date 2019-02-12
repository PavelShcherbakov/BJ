using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IDeckRepository : IRepository<Card, Guid>
    {
        Task<IEnumerable<Card>> GetRandomCardsByGameId(Guid gameId, int numOfCards);
        int GetCountCards(Guid gameId);
    }
}
