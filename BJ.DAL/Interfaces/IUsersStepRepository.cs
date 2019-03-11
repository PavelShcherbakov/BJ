using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IUsersStepRepository : IRepository<UsersStep, Guid>
    {
        Task<IEnumerable<UsersStep>> GetStepsByGameIdAsync(Guid gameId);
    }
}
