using System;

namespace Cifra.ConsoleHost
{
    public class DateTimeProvider
    {
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
