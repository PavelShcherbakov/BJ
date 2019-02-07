using BJ.BLL.Helpers;
using BJ.BLL.Services;
using BJ.DAL.Repositories;
using BJ.DAL.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configrutions
{

    public static class DependencyInjectionConfigExtension
    {
        public static void Inject(this IServiceCollection services)
        {
            services.AddTransient<JwtTokenHelper>();
            services.AddTransient<AccountService>();
            services.AddTransient<UnitOfWork>();

            services.AddScoped<EFBotRepository,>
            
        }
    }
}
