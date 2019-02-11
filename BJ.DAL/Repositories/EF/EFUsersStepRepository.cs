using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersStepRepository : EFGenericRepository<UsersStep, Guid>, IUsersStepRepository
    {
        public EFUsersStepRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
