using BJ.BLL.Exceptions;
using BJ.BLL.Providers;
using BJ.BLL.Providers.Interfaces;
using BJ.BLL.Services;
using BJ.BLL.Services.Interfaces;
using BJ.DAL.Interfaces;
using BJ.DAL.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configrutions
{

    public static class DependencyInjectionConfig
    {

        public static void ConfigureDependencyInjection(this IServiceCollection services, string dbType)
        {


            services.AddTransient<ITokenProvider, JwtTokenProvider>();
            

            //if (configuration.GetValue<string>("ORM") == "EF") if (configuration.GetValue<string>("ORM") == "EF")
            if (dbType == "EF")
            {
                services.AddTransient<IBotRepository, EFBotRepository>();
                services.AddTransient<IBotsStepRepository, EFBotsStepRepository>();
                services.AddTransient<IDeckRepository, EFDeckRepository>();
                services.AddTransient<IGameRepository, EFGameRepository>();
                services.AddTransient<IUserRepository, EFUserRepository>();
                services.AddTransient<IUsersStepRepository, EFUsersStepRepository>();
                services.AddTransient<IUsersPointsRepository, EFUsersPointsRepository>();
                services.AddTransient<IBotsPointsRepository, EFBotsPointsRepository>();
            }
            else
            {
                throw new CustomServiceException("ORM not defined");
            }


            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IGameService, GameService>();
            services.AddTransient<IHistoryService, HistoryService>();

        }
    }
}
