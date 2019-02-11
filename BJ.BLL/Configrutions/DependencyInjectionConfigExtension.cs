using BJ.BLL.Exceptions;
using BJ.BLL.Helpers;
using BJ.BLL.Services;
using BJ.DAL.Interfaces;
using BJ.DAL.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace BJ.BLL.Configrutions
{

    public static class DependencyInjectionConfigExtension
    {
        
        public static void Inject(this IServiceCollection services, string dbType)
        {

            services.AddTransient<JwtTokenHelper>();
            services.AddTransient<AccountService>();

            //if (configuration.GetValue<string>("ORM") == "EF") if (configuration.GetValue<string>("ORM") == "EF")
            if (dbType == "EF")
            {
                services.AddTransient<IBotRepository, EFBotRepository>();
                services.AddTransient<IBotsStepRepository, EFBotsStepRepository>();
                services.AddTransient<IDeckRepository, EFDeckRepository>();
                services.AddTransient<IGameRepository, EFGameRepository>();
                services.AddTransient<IUserRepository, EFUserRepository>();
                services.AddTransient<IUsersStepRepository, EFUsersStepRepository>();
            }
            else
            {
                throw new CustomServiceException("ORM not defined");
            }

        }
    }
}
