﻿using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Autofac;
using Microsoft.Extensions.Configuration;

namespace Cifra.ConsoleHost
{
    /// <summary>
    /// The technical starting point of the application.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal class Program
    {
        internal static async Task Main(string[] args)
        {
            Console.BackgroundColor = ConsoleColor.Yellow;
            Console.ForegroundColor = ConsoleColor.Black;
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
