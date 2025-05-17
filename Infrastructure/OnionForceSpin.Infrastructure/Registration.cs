using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using OnionForceSpin.Infrastructure.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Infrastructure
{
    public static class Registration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(options =>
            {
                configuration.GetSection("JWT").Bind(options);
            });

        }
    }
}
