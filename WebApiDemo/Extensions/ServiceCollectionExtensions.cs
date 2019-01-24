using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddScopedDynamic<TInterface>(this IServiceCollection services, List<Type> types)
        {
            services.AddScoped<Func<string, TInterface>>(serviceProvider => tenant =>
            {
                var type = types.Where(y => y.GetInterfaces().Contains(typeof(TInterface)))
                                          .Where(x => x.Name.Contains(tenant))
                                          .FirstOrDefault();
                if (null == type)
                    throw new KeyNotFoundException("Aucune instance trouvée pour le tenant fournit.");

                return (TInterface)serviceProvider.GetService(type);
            });
        }
    }
}
