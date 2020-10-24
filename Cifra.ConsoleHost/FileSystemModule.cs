﻿using Autofac;
using Cifra.ConsoleHost.Areas.Class;
using Cifra.ConsoleHost.Areas.Spreadsheet;
using Cifra.ConsoleHost.Areas.Test;

namespace Cifra.ConsoleHost
{
    public class FileSystemModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClassMenuFlow>();
            builder.RegisterType<CreateClassFlow>();
            builder.RegisterType<EditClassFlow>();
            builder.RegisterType<DeleteClassFlow>();
            builder.RegisterType<SpreadsheetMenuFlow>();
            builder.RegisterType<CreateSpreadsheetFlow>();
            builder.RegisterType<TestMenuFlow>();
            builder.RegisterType<CreateTestFlow>();
            builder.RegisterType<EditTestFlow>();
            builder.RegisterType<DeleteTestFlow>();
        }
    }
}
