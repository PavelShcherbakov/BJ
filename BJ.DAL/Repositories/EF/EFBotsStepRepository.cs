using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsStepRepository : EFGenericRepository<BotsStep, Guid>, IBotsStepRepository
    {
        public EFBotsStepRepository(ApplicationDbContext context) : base(context)
        {

        }

        override public IEnumerable<BotsStep> Find(Func<BotsStep, bool> predicate)
        {
            var result = _dbSet.AsNoTracking().Include(x => x.Bot).Where(predicate).ToList();
            return result;
        }
    }
}
