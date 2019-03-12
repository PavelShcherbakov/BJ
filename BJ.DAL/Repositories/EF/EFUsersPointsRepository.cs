using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersPointsRepository : EFGenericRepository<UsersPoints, Guid>, IUsersPointsRepository
    {
        public EFUsersPointsRepository(ApplicationDbContext context) : base(context) { }

        public async Task<UsersPoints> GetPointsByGameIdAsync(Guid gameId)
        {
            var result = await _dbSet.Where(x => x.GameId == gameId).FirstOrDefaultAsync();
            return result;
        }
    }
}
