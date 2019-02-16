using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiDemo.HttpClients
{
    public class DataClient : IDataClient
    {
        private HttpClient _client;

        public DataClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<object> GetData()
        {
            return (await _client.GetAsync("DemoException/error")).Content.ReadAsStringAsync();
        }
    }
}
