using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BJ.DAL.Repositories.EF
{
    public class EFBotsStepRepository : IBotsStepRepository
    {
        private ApplicationDbContext _db;

        public EFBotsStepRepository(ApplicationDbContext context)
        {
            this._db = context;
        }

        public void Create(BotsStep item)
        {
            _db.BotsSteps.Add(item);
        }

        public BotsStep Get(int id)
        {
            return _db.BotsSteps.Find(id);
        }

        public IEnumerable<BotsStep> Find(Func<BotsStep, bool> predicate)
        {
            return _db.BotsSteps.Where(predicate).ToList();
        }

        public IEnumerable<BotsStep> GetAll()
        {
            return _db.BotsSteps;
        }

        public void Update(BotsStep item)
        {
            _db.Entry(item).State = EntityState.Modified;
        }

        public void Delete(int id)
        {
            BotsStep botsStep = _db.BotsSteps.Find(id);
            if (botsStep != null)
                _db.BotsSteps.Remove(botsStep);
        }
    }
}
