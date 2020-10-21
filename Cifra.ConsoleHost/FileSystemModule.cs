using Autofac;
using Cifra.ConsoleHost.Areas.Spreadsheet;
using Cifra.ConsoleHost.Areas.Test;
using Cifra.ConsoleHost.Functionalities.Class;
using System;
using System.Collections.Generic;
using System.Text;

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
            builder.RegisterType<TestMenuFlow>();
        }
    }
}
