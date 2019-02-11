using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFUserRepository : EFGenericRepository<User, string>, IUserRepository
    {
        public EFUserRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}


