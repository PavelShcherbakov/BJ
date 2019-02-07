using BJ.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Game> Games { get; }
        IRepository<User> Users { get; }
        IRepository<UsersStep> UsersSteps { get; }
        IRepository<Bot> Bots { get; }
        IRepository<BotsStep> BotsSteps { get; }
        IRepository<Deck> Decks { get; }
        IRepository<Card> Cards { get; }
        void Save();
    }
}
