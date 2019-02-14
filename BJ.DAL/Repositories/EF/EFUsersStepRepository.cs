using BJ.DAL.Interfaces;
using BJ.Entities;
using System;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersStepRepository : EFGenericRepository<UsersStep, Guid>, IUsersStepRepository
    {
        public EFUsersStepRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
