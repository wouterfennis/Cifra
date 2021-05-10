using Autofac;
using Cifra.ConsoleHost.Istall;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    /// <summary>
    /// The technical starting point of the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.ResetColor();

            IConfigurationRoot configuration = LoadConfiguration();

            await Installation.Start(configuration);

            var container = DependencyInjection.RegisterDependencies(configuration.GetSection("Appsettings"));
            var application = container.Resolve<Application>();
            await application.StartAsync();
        }

        private static IConfigurationRoot LoadConfiguration()
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile(path: "./appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            return configuration;
        }
    }
}
