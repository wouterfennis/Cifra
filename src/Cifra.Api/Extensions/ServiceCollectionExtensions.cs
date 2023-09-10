using Cifra.Api.Mapping;
using Cifra.Application.Extensions;
using Cifra.Database.Mapping;
using Cifra.Database.Extensions;
using Cifra.FileSystem.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

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
            string databasename = configuration.GetSection("ConnectionStrings").GetValue<string>("Sqlite");
            services.SetupDatabaseDependencies(databasename);
            services.SetupFileSystemDependencies(configuration);

            services.AddAutoMapper(Assembly.GetExecutingAssembly(), Assembly.GetAssembly(typeof(DatabaseProfile)), Assembly.GetAssembly(typeof(ApiProfile)));
        }
    }
}



