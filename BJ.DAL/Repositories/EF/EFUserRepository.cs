using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFUserRepository : IRepository<User>
    {
        private ApplicationDbContext _db;

        public EFUserRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(User item)
        {
            _db.Users.Add(item);
        }

        public User Get(int id)
        {
            return _db.Users.Find(id);
        }

        public IEnumerable<User> Find(Func<User, bool> predicate)
        {
            return _db.Users.Where(predicate).ToList();
        }

        public IEnumerable<User> GetAll()
        {
            return _db.Users;
        }

        public void Update(User item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            User user = _db.Users.Find(id);
            if (user != null)
                _db.Users.Remove(user);
        }
    }
}


