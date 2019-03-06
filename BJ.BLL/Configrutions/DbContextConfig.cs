using BJ.BLL.Configrutions.Options;
using BJ.DAL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;

namespace BJ.BLL.Configrutions
{
    public static class DbContextConfig
    {
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            var initializeConnectionString = configuration.GetSection("DbOptions").Get<DbOptions>().InitializeConnectionString;
            var connectionString = configuration.GetConnectionString(initializeConnectionString);

            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}
