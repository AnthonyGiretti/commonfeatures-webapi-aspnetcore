using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Providers
{
    public interface IServicesProvider<TInterface>
    {
        TInterface GetInstance(string key);
    }
}
