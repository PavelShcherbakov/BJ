using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;
using BJ.DAL.Interfaces;
using BJ.Entities;
using DapperExtensions;
using Microsoft.Extensions.Configuration;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperBotsStepRepository : DapperGenericRepository<BotsStep, Guid>, IBotsStepRepository
    {
        public DapperBotsStepRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<BotsStep>> GetStepsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                var predicate = Predicates.Field<BotsStep>(f => f.GameId, Operator.Eq, gameId);
                conn.Open();
                var result = await conn.GetListAsync<BotsStep>(predicate);
                return result;
            }
        }
    }
}
