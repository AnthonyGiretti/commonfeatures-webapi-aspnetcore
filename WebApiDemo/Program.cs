using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
                    new HostBuilder()
                    .UseContentRoot(Directory.GetCurrentDirectory())
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel()
                                  .UseSerilog()
                                  .UseStartup<Startup>();
                    }).ConfigureAppConfiguration((context, config) =>
                    {
                        config.SetBasePath(Directory.GetCurrentDirectory())
                              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                              .AddEnvironmentVariables()
                              .AddUserSecrets<Startup>();

                        var builtConfig = config.Build();
                        config.AddAzureKeyVault(
                            $"https://{builtConfig["KeyVault:Vault"]}.vault.azure.net/",
                            builtConfig["KeyVault:ClientId"],
                            builtConfig["KeyVault:ClientSecret"],
                            new DefaultKeyVaultSecretManager());
                    }).ConfigureLogging((logging) =>
                    {
                        //logging.AddConsole();
                        //logging.AddDebug();
                    });
    }
}