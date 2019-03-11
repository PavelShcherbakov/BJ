using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotsPointsRepository : IRepository<BotsPoints, Guid>
    {
        Task<IEnumerable<BotsPoints>> GetPointsByGameIdAsync(Guid gameId);
    }
}
