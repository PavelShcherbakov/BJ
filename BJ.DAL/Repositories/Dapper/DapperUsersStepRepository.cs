using BJ.DAL.Interfaces;
using BJ.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperUsersStepRepository : DapperGenericRepository<UsersStep,Guid>, IUsersStepRepository
    {
        public DapperUsersStepRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<UsersStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                var predicate = Predicates.Field<UsersStep>(f => f.GameId, Operator.Eq, gameId);
                conn.Open();
                var result = await conn.GetListAsync<UsersStep>(predicate);
                return result;
            }
        }
    }
}
