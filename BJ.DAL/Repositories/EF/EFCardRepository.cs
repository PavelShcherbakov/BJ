using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFCardRepository : IRepository<Card>
    {
        private ApplicationDbContext _db;

        public EFCardRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(Card item)
        {
            _db.Cards.Add(item);
        }

        public Card Get(int id)
        {
            return _db.Cards.Find(id);
        }

        public IEnumerable<Card> Find(Func<Card, bool> predicate)
        {
            return _db.Cards.Where(predicate).ToList();
        }

        public IEnumerable<Card> GetAll()
        {
            return _db.Cards;
        }

        public void Update(Card item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            Card card = _db.Cards.Find(id);
            if (card != null)
                _db.Cards.Remove(card);
        }
    }
}
