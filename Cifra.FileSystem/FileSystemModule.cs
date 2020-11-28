using Autofac;
using Cifra.FileSystem.FileReaders;
using Cifra.FileSystem.Repositories;
using Cifra.FileSystem.Spreadsheet;

namespace Cifra.FileSystem
{
    public class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClassRepository>().AsImplementedInterfaces();
            builder.RegisterType<TestResultsSpreadsheetFactory>().AsImplementedInterfaces();
            builder.RegisterType<FileInfoWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<DirectoryInfoWrapperFactory>().AsImplementedInterfaces();
            builder.RegisterType<MagisterFileReader>().AsImplementedInterfaces();
        }
    }
}
