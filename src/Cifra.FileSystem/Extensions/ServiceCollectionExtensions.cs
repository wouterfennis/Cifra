using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Spreadsheet;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SpreadsheetWriter.Abstractions.File;
using SpreadsheetWriter.Abstractions.Formula;
using SpreadsheetWriter.EPPlus.File;
using SpreadsheetWriter.EPPlus.Formula;
using System.Diagnostics.CodeAnalysis;

namespace Cifra.FileSystem.Extensions
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
        public static void SetupFileSystemDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            var fileLocationProvider = new FileLocationProvider(null, null, null, null);
            services.AddSingleton<IFileLocationProvider>(fileLocationProvider);
            services.AddScoped<IFileInfoWrapperFactory, FileInfoWrapperFactory>();
            services.AddScoped<IDirectoryInfoWrapperFactory, DirectoryInfoWrapperFactory>();
            services.AddScoped<ITestResultsSpreadsheetBuilder, TestResultsSpreadsheetBuilder>();
            services.AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>();
            services.AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>();
            services.AddScoped<IFormulaBuilderFactory, FormulaBuilderFactory>();
            services.AddScoped<IFormulaBuilder, FormulaBuilder>();
        }
    }
}
