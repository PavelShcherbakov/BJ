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
    public class DapperUsersPointsRepository : DapperGenericRepository<UsersPoints,Guid>, IUsersPointsRepository
    {
        public DapperUsersPointsRepository(IConfiguration config) : base(config)
        {
        }

        public async Task<UsersPoints> GetPointsByGameIdAsync(Guid gameId)
        {
            using (IDbConnection conn = Connection)
            {
                var predicate = Predicates.Field<UsersPoints>(f => f.GameId, Operator.Eq, gameId);
                conn.Open();
                var result = await conn.GetAsync<UsersPoints>(predicate);
                return result;
            }
        }
    }
}
