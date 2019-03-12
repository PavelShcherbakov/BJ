using BJ.DAL.Interfaces;
using BJ.Entities;

namespace BJ.DAL.Repositories.EF
{
    public class EFUserRepository : EFGenericRepository<User, string>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext context) : base(context) { }
    }
}


