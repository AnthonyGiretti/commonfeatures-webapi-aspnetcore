using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace WebApiDemo.HttpClients
{
    public class StreamingClient : IStreamingClient
    {
        private HttpClient _client;

        public StreamingClient(HttpClient client)
        {
            _client = client;
        }

        public async Task<Stream> GetStream(string name)
        {
            var urlBlob = string.Empty;
            switch (name)
            {
                case "earth":
                    urlBlob = "earth.mp4";
                    break;
                case "nature1":
                    urlBlob = "nature1.mp4";
                    break;
                case "nature2":
                default:
                    urlBlob = "nature2.mp4";
                    break;

            }
            return await _client.GetStreamAsync(urlBlob);
        }
    }
}