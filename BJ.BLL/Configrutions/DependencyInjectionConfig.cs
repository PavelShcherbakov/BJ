using BJ.BLL.Exceptions;
using BJ.BLL.Providers;
using BJ.BLL.Providers.Interfaces;
using BJ.BLL.Services;
using BJ.BLL.Services.Interfaces;
using BJ.DAL.Interfaces;
using BJ.DAL.Repositories.EF;
using BJ.BLL.Configrutions.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System;

namespace BJ.BLL.Configrutions
{

    public static class DependencyInjectionConfig
    {

        public static void ConfigureDependencyInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddTransient<ITokenProvider, JwtTokenProvider>();
            
            var dbOptions = configuration.GetSection("DbOptions").Get<DbOptions>();

            if (dbOptions.ORM == "EF")
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
