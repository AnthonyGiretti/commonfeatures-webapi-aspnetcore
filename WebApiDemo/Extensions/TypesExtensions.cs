using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Extensions
{
    public static class TypesExtensions
    {
        public static IEnumerable<Type> FilterByInterface<TInterface>(this IEnumerable<Type> types)
        {
            return types.Where(y => y.GetInterfaces().Contains(typeof(TInterface)));
        }
    }
}
