using BJ.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BJ.BLL.Configrutions
{
    public static class DbInitiolazeConfigExtension
    {
        public static void InitiolazeDb(this IServiceCollection services)
        {
            Initialize(services.BuildServiceProvider());
        }

        public static void Initialize(IServiceProvider service)
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
