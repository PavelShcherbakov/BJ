using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotRepository : EFGenericRepository<Bot,Guid>, IBotRepository
    {
        public EFBotRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
