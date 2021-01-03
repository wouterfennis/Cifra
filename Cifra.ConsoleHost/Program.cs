using Autofac;
using Microsoft.Extensions.Configuration;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost
{
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine("This is a test version. Not ready for actual use");
            Console.ResetColor();
            IConfigurationSection appsettings = SetupAppsettings();
            var container = DependencyInjection.RegisterDependencies(appsettings);
            var application = container.Resolve<Application>();
            await application.StartAsync();
        }

        private static IConfigurationSection SetupAppsettings()
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Environment.CurrentDirectory)
                .AddJsonFile("./appsettings.json", false, true)
                .Build();

            return configuration.GetSection("Appsettings");
        }
    }
}
