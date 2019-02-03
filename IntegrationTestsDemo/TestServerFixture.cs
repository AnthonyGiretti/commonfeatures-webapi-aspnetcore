using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.Net.Http;
using WebApiDemo;

namespace IntegrationTestsDemo
{
    public class TestServerFixture : IDisposable
    {
        private readonly TestServer _testServer;
        public HttpClient Client { get; }

        public TestServerFixture()
        {
            var builder = new WebHostBuilder()
                   .UseEnvironment("Development")
                   .UseStartup<Startup>();

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();

        }

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}