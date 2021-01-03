﻿using Autofac;
using Cifra.FileSystem.FileReaders;
using Cifra.FileSystem.FileSystemInfo;
using Cifra.FileSystem.Repositories;
using Cifra.FileSystem.Spreadsheet;
using SpreadsheetWriter.EPPlus.File;
using SpreadsheetWriter.EPPlus.Formula;

namespace Cifra.FileSystem
{
    public class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<TestRepository>().AsImplementedInterfaces();
            builder.RegisterType<ClassRepository>().AsImplementedInterfaces();
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
