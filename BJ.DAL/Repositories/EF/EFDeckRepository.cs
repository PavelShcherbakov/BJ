using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFDeckRepository : EFGenericRepository<Deck, Guid>, IDeckRepository
    {
        public EFDeckRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
