using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IBotsStepRepository : IRepository<BotsStep, Guid>
    {
        Task<IEnumerable<BotsStep>> GetStepsByGameIdAsync(Guid gameId);
    }

}
