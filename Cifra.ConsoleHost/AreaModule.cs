﻿using Autofac;
using Cifra.ConsoleHost.Areas.Class;
using Cifra.ConsoleHost.Areas.Spreadsheet;
using Cifra.ConsoleHost.Areas.Test;

namespace Cifra.ConsoleHost
{
    public class AreaModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<ClassMenuFlow>();
            builder.RegisterType<CreateClassManuallyFlow>();
            builder.RegisterType<CreateClassFromMagisterFlow>();
            builder.RegisterType<SpreadsheetMenuFlow>();
            builder.RegisterType<CreateSpreadsheetFlow>();
            builder.RegisterType<TestMenuFlow>();
            builder.RegisterType<CreateTestFlow>();
        }
    }
}
