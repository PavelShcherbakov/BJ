using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsPointsRepository : EFGenericRepository<BotsPoints, Guid>, IBotsPointsRepository
    {
        public EFBotsPointsRepository(ApplicationDbContext context) : base(context)
        {
        }
        override public IEnumerable<BotsPoints> Find(Func<BotsPoints, bool> predicate)
        {
            var result = _dbSet.AsNoTracking().Include(x => x.Bot).Where(predicate).ToList();
            return result;
        }
    }
}
