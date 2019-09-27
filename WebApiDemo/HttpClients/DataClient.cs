using Microsoft.Extensions.Logging;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiDemo.HttpClients
{
    public class DataClient : IDataClient
    {
        private HttpClient _client;
        private ILogger<DataClient> _logger;

        public DataClient(HttpClient client, ILogger<DataClient> logger)
        {
            _client = client;
            _logger = logger;
        }

        public async Task<object> GetData()
        {
            _logger.LogInformation("Trying to log data");
            return await (await _client.GetAsync("DemoException/error")).Content.ReadAsStringAsync();
        }
    }
}
