using Cifra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.Database.Extensions
{
    /// <summary>
    /// Static class for registering dependencies.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup dependencies.
        /// </summary>
        public static void SetupDatabaseDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            string databaseConnectionString = configuration.GetSection("ConnectionStrings").GetValue<string>("Sqlite");
            services.AddDbContext<Context>(options => options.UseSqlite(databaseConnectionString));
            services.AddScoped<ITestRepository, TestDatabaseRepository>();
            services.AddScoped<IClassRepository, ClassDatabaseRepository>();
        }
    }
}
