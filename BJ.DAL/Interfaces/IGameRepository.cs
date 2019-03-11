using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game, Guid>
    {
        Task<Game> GetActiveGameAsync(string userId);
        Task<IEnumerable<Game>> GetСompletedGamesAsync(string userId);
    }
}
