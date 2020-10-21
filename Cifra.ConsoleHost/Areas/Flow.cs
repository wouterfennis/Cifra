using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas
{
    internal interface IFlow
    {
        public Task StartAsync();
    }
}
