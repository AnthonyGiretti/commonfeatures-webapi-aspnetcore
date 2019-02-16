using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.HttpClients
{
    public interface IDataClient
    {
        Task<object> GetData();
    }
}
