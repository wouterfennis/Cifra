using Cifra.Application;
using Cifra.Application.Models.Class.Requests;
using Cifra.Application.Models.Class.Results;
using Cifra.ConsoleHost.Areas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas.Class
{
    internal class CreateClassFromMagisterFlow : IFlow
    {
        private readonly ClassController _classController;

        public CreateClassFromMagisterFlow(ClassController classController)
        {
            _classController = classController;
        }

        public async Task StartAsync()
        {
        }
    }
}
