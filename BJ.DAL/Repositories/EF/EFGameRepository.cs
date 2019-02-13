using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.EF
{
    public class EFGameRepository : EFGenericRepository<Game, Guid>, IGameRepository
    {
        public EFGameRepository(ApplicationDbContext context) : base(context)
        {
        }

        //public async Task<int> GetCountStepAsync(Guid id)
        //{
        //    var game = await _dbSet.FindAsync(id);
        //    var result = game.CountStep;
        //    return result;
        //}
    }
}
