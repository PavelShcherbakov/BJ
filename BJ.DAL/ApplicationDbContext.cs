using BJ.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BJ.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Game> Games { get; set; }
        public DbSet<Bot> Bots { get; set; }
        public DbSet<BotsStep> BotsSteps { get; set; }
        public DbSet<UsersStep> UsersSteps { get; set; }
        public DbSet<Card> Decks { get; set; }
        public DbSet<BotsPoints> BotsPoints { get; set; }
        public DbSet<UsersPoints> UsersPoints { get; set; }
    }
}
