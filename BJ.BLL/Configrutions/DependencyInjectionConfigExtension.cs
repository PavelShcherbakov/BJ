using BJ.BLL.Helpers;
using BJ.BLL.Services;
using Microsoft.Extensions.DependencyInjection;

namespace BJ.BLL.Configrutions
{

    public static class DependencyInjectionConfigExtension
    {
        public static void Inject(this IServiceCollection services)
        {
            services.AddTransient<AccountService>();
            services.AddTransient<JwtTokenHelper>();
        }
    }
}
