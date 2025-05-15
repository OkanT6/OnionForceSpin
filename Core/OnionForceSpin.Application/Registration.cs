using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using OnionForceSpin.Application.Bases;
using OnionForceSpin.Application.Behaviors;
using OnionForceSpin.Application.CustomMiddlewares;
using OnionForceSpin.Application.Exceptions;
using OnionForceSpin.Application.Features.Products.Rules;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnionForceSpin.Application
{
    public static class Registration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddTransient<RequestLoggerMiddleware>();

            services.AddTransient<ExceptionMiddleware>();

            //services.AddTransient<ProductRules>(); böyle tek tek rule'ları eklemek yerine aşağıdaki kodu ekliyorum:
            services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

            //services.AddRulesFromAssemblyContaining(assembly, typeof(BaseRules));

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");    

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehevior<,>));

        }

        private static IServiceCollection AddRulesFromAssemblyContaining(this IServiceCollection services,
            Assembly assembly,
            Type type)
        {
            var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();

            foreach (var item in types)
                services.AddTransient(item);

            return services;
        }


        //private static IServiceCollection AddRulesFromAssemblyContaining(
        //    this IServiceCollection services,
        //    Assembly assembly,
        //    Type type)
        //{
        //    var types = assembly.GetTypes().Where(t => t.IsSubclassOf(type) && type != t).ToList();
        //    foreach (var item in types)
        //        services.AddTransient(item);

        //    return services;
        //}
    }
}
