using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Options;
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
    [ExcludeFromCodeCoverage] // DI setup
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Setup dependencies.
        /// </summary>
        public static void SetupFileSystemDependencies(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .Configure<SpreadsheetOptions>(options => configuration.GetSection(SpreadsheetOptions.Section)
                .Bind(options));

            services
                .AddScoped<IFileInfoWrapperFactory, FileInfoWrapperFactory>()
                .AddScoped<IDirectoryInfoWrapperFactory, DirectoryInfoWrapperFactory>()
                .AddScoped<ITestResultsSpreadsheetBuilder, TestResultsSpreadsheetBuilder>()
                .AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>()
                .AddScoped<ISpreadsheetFileFactory, ExcelFileFactory>()
                .AddScoped<IFormulaBuilderFactory, FormulaBuilderFactory>()
                .AddScoped<IFormulaBuilder, FormulaBuilder>();
        }
    }
}
