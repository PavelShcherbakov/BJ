using BJ.DAL.Interfaces;
using BJ.Entities;
using Dapper;
using DapperExtensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace BJ.DAL.Repositories.Dapper
{
    public class DapperBotsPointsRepository : DapperGenericRepository<BotsPoints, Guid>, IBotsPointsRepository
    {
        public DapperBotsPointsRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<IEnumerable<BotsPoints>> GetPointsByGameIdAsync(Guid gameId)
        {
            
            using (IDbConnection conn = Connection)
            {
                var predicate = Predicates.Field<BotsPoints>(f => f.GameId, Operator.Eq, gameId);
                conn.Open();
                var result = await conn.GetListAsync<BotsPoints>(predicate);
                return result;
            }
        }
    }
}
