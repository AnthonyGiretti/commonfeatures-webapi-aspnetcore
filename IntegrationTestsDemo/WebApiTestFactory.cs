using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System.IO;
using WebApiDemo;

namespace IntegrationTestsDemo
{
    public class WebApiTestsFactory : WebApplicationFactory<StartupTest>
    {
        protected override IHostBuilder CreateHostBuilder()
        {
            return new HostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel()
                                  .UseStartup<StartupTest>();
                    });
        }

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }
    }
    
}
