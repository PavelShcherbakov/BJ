using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJ.DAL.Repositories.EF
{
    public class EFGameRepository : IRepository<Game>
    {
        private ApplicationDbContext _db;

        public EFGameRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(Game item)
        {
            _db.Games.Add(item);
        }

        public Game Get(int id)
        {
            return _db.Games.Find(id);
        }

        public IEnumerable<Game> Find(Func<Game, bool> predicate)
        {
            return _db.Games.Where(predicate).ToList();
        }

        public IEnumerable<Game> GetAll()
        {
            return _db.Games;
        }

        public void Update(Game item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Game game = _db.Games.Find(id);
            if (game != null)
                _db.Games.Remove(game);
        }    
    }
}
