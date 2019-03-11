using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersStepRepository : EFGenericRepository<UsersStep, Guid>, IUsersStepRepository
    {
        public EFUsersStepRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<UsersStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            var result = await _dbSet.Where(x => x.GameId == gameId).ToListAsync();
            return result;
        }
    }
}
