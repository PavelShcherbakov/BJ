using BJ.DAL.Interfaces;
using BJ.DAL.Repositories.EF;
using BJ.Entities;
using System;

namespace BJ.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private ApplicationDbContext _db;
        private IRepository<Game> _gameRepository;
        private IRepository<User> _userRepository;
        private IRepository<UsersStep> _usersStepRepository;
        private IRepository<Bot> _botRepository;
        private IRepository<BotsStep> _botsStepRepository;
        private IRepository<Deck> _deckRepository;
        private IRepository<Card> _cardRepository;

        //private ApplicationDbContext _db;
        //private EFGameRepository _gameRepository;
        //private EFUserRepository _userRepository;
        //private EFUsersStepRepository _usersStepRepository;
        //private EFBotRepository _botRepository;
        //private EFBotsStepRepository _botsStepRepository;
        //private EFDeckRepository _deckRepository;
        //private EFCardRepository _cardRepository;

        public UnitOfWork(ApplicationDbContext applicationDbContext)
        {
            _db = applicationDbContext;
        }

        public IRepository<Game> Games
        {
            get
            {
                if (_gameRepository == null)
                    _gameRepository = new EFGameRepository(_db);                    
                return _gameRepository;
            }
        }

        public IRepository<User> Users
        {
            get
            {
                if (_userRepository == null)
                    _userRepository = new EFUserRepository(_db);
                return _userRepository;
            }
        }

        public IRepository<UsersStep> UsersSteps
        {
            get
            {
                if (_usersStepRepository == null)
                    _usersStepRepository = new EFUsersStepRepository(_db);
                return _usersStepRepository;
            }
        }

        public IRepository<Bot> Bots
        {
            get
            {
                if (_botRepository == null)
                    _botRepository = new EFBotRepository(_db);
                return _botRepository;
            }
        }

        public IRepository<BotsStep> BotsSteps
        {
            get
            {
                if (_botsStepRepository == null)
                    _botsStepRepository = new EFBotsStepRepository(_db);
                return _botsStepRepository;
            }
        }

        public IRepository<Deck> Decks
        {
            get
            {
                if (_deckRepository == null)
                    _deckRepository = new EFDeckRepository(_db);
                return _deckRepository;
            }
        }

        public IRepository<Card> Cards
        {
            get
            {
                if (_cardRepository == null)
                    _cardRepository = new EFCardRepository(_db);
                return _cardRepository;
            }
        }

        public void Save()
        {
            _db.SaveChanges();
        }

        private bool disposed = false;

        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _db.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
