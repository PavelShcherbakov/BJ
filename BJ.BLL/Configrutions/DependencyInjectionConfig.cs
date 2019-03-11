using BJ.BLL.Configrutions.Options;
using BJ.BLL.Services;
using BJ.DAL.Repositories.EF;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configrutions
{
    public static class DependencyInjectionConfig
    {
        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            var dbOptions = configuration.GetSection("DbOptions").Get<DbOptions>();

            var namespaceRepository = (dbOptions.ORM == "EF") ? "BJ.DAL.Repositories.EF" : "BJ.DAL.Repositories.Dapper";

            services.Scan(scan => scan
                .FromAssemblyOf<EFBotRepository>()
                    .AddClasses(classes => classes.InNamespaces(namespaceRepository))
                    .AsImplementedInterfaces()
                    .WithTransientLifetime()
            );

            services.Scan(scan => scan
                .FromAssemblyOf<AccountService>()

                   .AddClasses(classes => classes.Where(type => type.Name.EndsWith("Service") ||
                                                                type.Name.EndsWith("Provider") ||
                                                                type.Name.EndsWith("Helper")))
                        .AsImplementedInterfaces()
                        .WithTransientLifetime()
            );
        }
    }
}

