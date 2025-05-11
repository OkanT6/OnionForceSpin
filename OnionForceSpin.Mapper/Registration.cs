using Microsoft.Extensions.DependencyInjection;
using OnionForceSpin.Application.Interfaces.AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Mapper
{
    public static class Registration
    {
        public static void AddCustomMapper(this IServiceCollection services)
        {
            services.AddSingleton<IMapper, OnionForceSpin.Mapper.AutoMapper.Mapper>(); 
        }
    }
}
