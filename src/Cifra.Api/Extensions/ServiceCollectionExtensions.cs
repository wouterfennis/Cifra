using Cifra.Api.Exceptions;
using Cifra.Application.Extensions;
using Cifra.Database.Extensions;
using Cifra.FileSystem.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.Api.Extensions
{
    /// <summary>
    /// Static class for registering dependencies.
    /// </summary>
    [ExcludeFromCodeCoverage]
    internal static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup dependencies.
        /// </summary>
        public static void SetupDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.SetupApplicationDependencies(configuration);
            string? databasename = configuration.GetSection("ConnectionStrings").GetValue<string>("Sqlite") ?? throw new InvalidConfigurationException("ConnectionStrings:Sqlite not found in appsettings.json");
            services.SetupDatabaseDependencies(databasename);
            services.SetupFileSystemDependencies(configuration);
        }
    }
}



