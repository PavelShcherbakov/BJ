using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFDeckRepository : EFGenericRepository<Card, Guid>, IDeckRepository
    {
        public EFDeckRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Card>> GetRandomCardsByGameId(Guid gameId, int numOfCards)
        {
            var decks = await _dbSet.Where(x => x.GameId == gameId).OrderBy(r => Guid.NewGuid()).Take(numOfCards).ToListAsync();
            return decks;
        }

        public async Task<int> GetCountCardsAsync(Guid gameId)
        {
            var count = await _dbSet.Where(x => x.GameId == gameId).CountAsync();
            return count;
        }

        public async Task<IEnumerable<Card>> GetCardsByGameIdAsync(Guid gameId)
        {
            var result = await _dbSet.Where(x => x.GameId == gameId).ToListAsync();
            return result;
        }
    }
}
