using System;

namespace Cifra.ConsoleHost.Utilities
{
    /// <summary>
    /// Provider for DateTime.
    /// </summary>
    public class DateTimeProvider
    {
        /// <summary>
        /// Gets the Current DateTime.
        /// </summary>
        public static Func<DateTime> Now = () => DateTime.Now;
    }
}
