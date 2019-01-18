using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.Services
{
    public interface IConfig
    {
        string ConnectionString { get; }
    }
}
