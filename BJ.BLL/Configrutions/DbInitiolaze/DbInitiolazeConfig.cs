using BJ.BLL.Configrutions.DbInitiolaze;
using BJ.DAL.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BJ.BLL.DbInitiolaze.Configrutions
{
    public static class DbInitiolazeConfig
    {
        public static void InitializeDatabaseAsync(this IServiceCollection services)
        {
            BotInitializeAsync(services.BuildServiceProvider());
        }

        public static void BotInitializeAsync(IServiceProvider service)
        {
            using (var serviceScope = service.CreateScope())
            {
                var scopeServiceProvider = serviceScope.ServiceProvider;
                var botRepository = scopeServiceProvider.GetService<IBotRepository>();
                BotsData.Initialize(botRepository);
            }
        }
    }
}
