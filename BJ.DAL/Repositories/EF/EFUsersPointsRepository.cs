using BJ.DAL.Interfaces;
using BJ.Entities;
using System;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersPointsRepository : EFGenericRepository<UsersPoints, Guid>, IUsersPointsRepository
    {
        public EFUsersPointsRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
