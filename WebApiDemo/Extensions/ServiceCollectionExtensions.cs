using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace WebApiDemo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedDynamic<TInterface>(this IServiceCollection services, IEnumerable<Type> types)
        {
            services.AddScoped<Func<string, TInterface>>(serviceProvider => tenant =>
            {
                var type = types.FilterByInterface<TInterface>()
                                .Where(x => x.Name.Contains(tenant))
                                .FirstOrDefault();

                if (null == type)
                    throw new KeyNotFoundException("No instance found for the given tenant.");

                return (TInterface)serviceProvider.GetService(type);
            });
        }
    }
}