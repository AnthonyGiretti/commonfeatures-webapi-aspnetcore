using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace WebApiDemo.HttpClients
{
    public interface IStreamingClient
    {
        Task<Stream> GetStream(string name);
    }
}
