using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsStepRepository : EFGenericRepository<BotsStep, Guid>, IBotsStepRepository
    {
        public EFBotsStepRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<BotsStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            var result = await _dbSet.Where(x => x.GameId == gameId).Include(x => x.Bot).ToListAsync();
            return result;
        }
    }
}
