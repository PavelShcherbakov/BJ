using BJ.Entities;
using System;
using System.Threading.Tasks;

namespace BJ.DAL.Interfaces
{
    public interface IGameRepository : IRepository<Game, Guid>
    {
        //Task<int> GetCountStepAsync(Guid id);
    }
}
