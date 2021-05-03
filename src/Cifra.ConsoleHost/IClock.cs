using System;

namespace Cifra.ConsoleHost
{
    internal interface IClock
    {
        DateTime Now { get; }
    }
}