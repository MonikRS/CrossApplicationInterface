using CrossApplicationInterface.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using System.IO;

namespace CrossApplicationInterface.Services
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ConfigurationBuilder();
            BuildConfiguration(builder);

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(builder.Build())
                .Enrich.FromLogContext()                
                .CreateLogger();

            var host = new HostBuilder()
                .ConfigureServices((context, services) =>
                {
                    services.AddHostedService<StartupService>();
                    services.AddTransient<ProcessJobs>();
                })
                .UseSerilog()
                .Build();

            host.Run();
        }

        public static void BuildConfiguration(IConfigurationBuilder builder)
        {
            builder.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();
        }
    }
}
