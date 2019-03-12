using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotRepository : EFGenericRepository<Bot, Guid>, IBotRepository
    {
        public EFBotRepository(ApplicationDbContext context) : base(context) { }

        public async Task<IEnumerable<Bot>> GetRandomBotsAsync(int numOfBots)
        {
            var bots = await _dbSet.OrderBy(r => Guid.NewGuid()).Take(numOfBots).ToListAsync();
            return bots;
        }


    }
}
