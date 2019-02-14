using BJ.DAL.Interfaces;
using BJ.Entities;
using System;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsPointsRepository : EFGenericRepository<BotsPoints, Guid>, IBotsPointsRepository
    {
        public EFBotsPointsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
