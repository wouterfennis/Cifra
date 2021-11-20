using Autofac;
using Cifra.FileSystem.FileReaders;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Spreadsheet;
using SpreadsheetWriter.EPPlus.File;
using SpreadsheetWriter.EPPlus.Formula;

namespace Cifra.FileSystem
{
    /// <summary>
    /// Dependency injection module for filesystem components
    /// </summary>
    public class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<FileInfoWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<DirectoryInfoWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<MagisterFileReader>().AsImplementedInterfaces();

            builder.RegisterType<TestResultsSpreadsheetBuilder>().AsImplementedInterfaces();
            builder.RegisterType<ExcelFileFactory>().AsImplementedInterfaces();
            builder.RegisterType<FormulaBuilderFactory>().AsImplementedInterfaces();
            builder.RegisterType<FormulaBuilder>().AsImplementedInterfaces();
        }
    }
}
