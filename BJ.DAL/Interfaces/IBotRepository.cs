using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotRepository : IRepository<Bot, Guid>
    {
        Task<IEnumerable<Bot>> GetRandomBotsAsync(int numOfBots);
    }
}
