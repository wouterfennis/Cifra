using Autofac;
using Cifra.ConsoleHost.Istall;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        private static ILogger _logger;

        internal static async Task Main(string[] args)
        {
            IConfigurationRoot configuration = LoadConfiguration();

            await Installation.Start(configuration);

            var containerBuilder = new ContainerBuilder();
            DependencyInjection.RegisterApplicationDependencies(containerBuilder, configuration.GetSection("Appsettings"));
            DependencyInjection.RegisterLogging(containerBuilder, configuration);
            IContainer container = containerBuilder.Build();
            var _logger = container.Resolve<ILogger>();

            AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionHandler;

            var application = container.Resolve<Application>();
            _logger.LogInformation("Application starting.");
            await application.StartAsync();
            _logger.LogInformation("Application stopped.");
        }

        static void UnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs e)
        {
            _logger.LogError((Exception)e.ExceptionObject, "Unhandled Exception occured");
            Environment.Exit(1);
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
