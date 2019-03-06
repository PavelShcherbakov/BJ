﻿using BJ.BLL.Configrutions.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace BJ.BLL.Configrutions
{
    public static class OptionsConfig
    {
        public static void ConfigureOptions(this IServiceCollection services, IConfiguration configuration)
        {      
            services.Configure<JwtOptions>(configuration.GetSection("JwtOptions"));
        }
    }
}
