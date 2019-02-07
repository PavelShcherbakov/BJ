using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFDeckRepository : IRepository<Deck>
    {
        private ApplicationDbContext _db;

        public EFDeckRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(Deck item)
        {
            _db.Decks.Add(item);
        }

        public Deck Get(int id)
        {
            return _db.Decks.Find(id);
        }

        public IEnumerable<Deck> Find(Func<Deck, bool> predicate)
        {
            return _db.Decks.Where(predicate).ToList();
        }

        public IEnumerable<Deck> GetAll()
        {
            return _db.Decks;
        }

        public void Update(Deck item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Deck deck = _db.Decks.Find(id);
            if (deck != null)
                _db.Decks.Remove(deck);
        }
    }
}
