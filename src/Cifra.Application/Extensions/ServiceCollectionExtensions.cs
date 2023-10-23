using Microsoft.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;

namespace Cifra.Application.Extensions
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
        public static void SetupApplicationDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<ITestService, TestService>();
            services.AddScoped<IClassService, ClassService>();
            services.AddScoped<ITestResultsSpreadsheetService, TestResultsSpreadsheetService>();
        }
    }
}
