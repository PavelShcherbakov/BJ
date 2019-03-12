using BJ.DAL.Interfaces;
using BJ.Entities;
using BJ.Entities.Enums;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFGameRepository : EFGenericRepository<Game, Guid>, IGameRepository
    {
        public EFGameRepository(ApplicationDbContext context) : base(context) { }

        public async Task<Game> GetActiveGameAsync(string userId)
        {
            var result = await _dbSet.Where(x => x.UserId == userId).FirstOrDefaultAsync(x => (int)x.State == (int)UserGameStateType.InGame);
            return result;
        }
        public async Task<IEnumerable<Game>> GetСompletedGamesAsync(string userId)
        {
            var result = await _dbSet.Where(x => x.UserId == userId && (int)x.State != (int)UserGameStateType.InGame).ToListAsync();
            return result;
        }
    }
}
