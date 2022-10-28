using Cifra.Database.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;

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
        public static void SetupDatabaseDependencies(this IServiceCollection services, string sqlitePath)
        {
            services.AddDbContext<Context>(options => options.UseSqlite($"DataSource={sqlitePath}"));
            services.AddScoped<ITestRepository, TestDatabaseRepository>();
            services.AddScoped<IClassRepository, ClassDatabaseRepository>();
        }
    }
}
