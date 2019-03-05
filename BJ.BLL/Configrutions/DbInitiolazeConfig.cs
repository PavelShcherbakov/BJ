using BJ.BLL.Commons;
using BJ.DAL.Interfaces;
using BJ.Entities;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BJ.BLL.Configrutions
{
    public static class DbInitiolazeConfig
    {
        public static void InitializeDatabase(this IServiceCollection services)
        {
            BotInitialize(services.BuildServiceProvider());
        }

        public static void BotInitialize(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var botRepository = scopeServiceProvider.GetService<IBotRepository>();
                BotsData.Initialize(botRepository);
            }
        }
    }

    static class BotsData
    {
        private static string[] _botsName = new string[] { "Olivia","Amelia","Isla","Emily","Ava","Lily","Mia", "Sofia", "Isabella","Grace",
                                                           "Oliver","Harry","Jack","George","Noah","Charlie", "Jacob","Alfie","Freddie","Oscar"};

        public static void Initialize(IBotRepository botRepository)
        {
            if (botRepository.Count(x => true) == 0)
            {
                List<Bot> bots = new List<Bot>(Constants.GameSettings.MaxCountBots);
                for (int i = 0; i < Constants.GameSettings.MaxCountBots; i++)
                {
                    var bot = new Bot()
                    {
                        Name = _botsName[i % _botsName.Count()]
                    };
                    bots.Add(bot);
                }
                botRepository.AddRange(bots);
            }
        }
    }
}
