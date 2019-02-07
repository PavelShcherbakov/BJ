using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFUsersStepRepository : IRepository<UsersStep>
    {
        private ApplicationDbContext _db;

        public EFUsersStepRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(UsersStep item)
        {
            _db.UsersSteps.Add(item);
        }

        public UsersStep Get(int id)
        {
            return _db.UsersSteps.Find(id);
        }

        public IEnumerable<UsersStep> Find(Func<UsersStep, bool> predicate)
        {
            return _db.UsersSteps.Where(predicate).ToList();
        }

        public IEnumerable<UsersStep> GetAll()
        {
            return _db.UsersSteps;
        }

        public void Update(UsersStep item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            UsersStep usersStep = _db.UsersSteps.Find(id);
            if (usersStep != null)
                _db.UsersSteps.Remove(usersStep);
        }
    }
}
