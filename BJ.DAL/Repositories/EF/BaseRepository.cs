using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    class BaseRepository:IRepository<BaseEntity>
    {
        private ApplicationDbContext _db;

        public BaseRepository(ApplicationDbContext context)
        {
            _db = context;
        }

        public void Create(BaseEntity entity)
        {
            //_db.Bots.Add(item);
            _db.Entry(entity).State = EntityState.Added;
        }

        public Bot Get(int id)
        {
            return _db.Bots.Find(id);
        }

        public IEnumerable<Bot> Find(Func<Bot, bool> predicate)
        {
            return _db.Bots.Where(predicate).ToList();
        }

        public IEnumerable<Bot> GetAll()
        {
            return _db.Bots;
        }

        public void Update(Bot item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Bot bot = _db.Bots.Find(id);
            if (bot != null)
                _db.Bots.Remove(bot);
        }
    }
}
