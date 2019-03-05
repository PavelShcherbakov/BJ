using BJ.DAL;
using BJ.DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BJ.BLL.Configrutions
{
    public static class DbContextConfig
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>();
        }
    }
}
