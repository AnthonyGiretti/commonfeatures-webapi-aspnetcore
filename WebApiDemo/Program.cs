using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.AzureKeyVault;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace WebApiDemo
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
                    Host.CreateDefaultBuilder(args)
                    .ConfigureWebHostDefaults(webBuilder =>
                    {
                        webBuilder.UseKestrel()
                                  .UseSerilog()
                                  .UseStartup<Startup>();
                    }).ConfigureAppConfiguration((context, config) =>
                    {
                        var builtConfig = config.Build();

                        config.AddAzureKeyVault(
                            $"https://{builtConfig["KeyVault:Vault"]}.vault.azure.net/",
                            builtConfig["KeyVault:ClientId"],
                            builtConfig["KeyVault:ClientSecret"],
                            new DefaultKeyVaultSecretManager());
                    }).ConfigureLogging((logging) =>
                    {
                        logging.AddSerilog();
                    });
    }
}