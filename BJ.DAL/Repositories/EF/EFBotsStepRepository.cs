using BJ.DAL.Interfaces;
using BJ.Entities;
using System;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsStepRepository : EFGenericRepository<BotsStep, Guid>, IBotsStepRepository
    {
        public EFBotsStepRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
