using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using System;
using System.IO;
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
                   .UseContentRoot(@"E:\Codes sources\Commonfeatures-webapi-aspnetcore\WebApiDemo")
                   .UseEnvironment("Development")
                   .UseStartup<Startup>();

            _testServer = new TestServer(builder);
            Client = _testServer.CreateClient();

        }

        //private string GetContentRootPath()
        //{
        //    var testProjectPath = PlatformServices.Default.Application.ApplicationBasePath;
        //    var relativePathToHostProject = @"WebApiDemo";
        //    return Path.Combine(testProjectPath, relativePathToHostProject);
        //}

        public void Dispose()
        {
            Client.Dispose();
            _testServer.Dispose();
        }
    }
}