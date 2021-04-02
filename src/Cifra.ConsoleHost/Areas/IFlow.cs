using System.Threading.Tasks;

namespace Cifra.ConsoleHost.Areas
{
    /// <summary>
    /// A user interface flow.
    /// </summary>
    internal interface IFlow
    {
        /// <summary>
        /// Starts flow.
        /// </summary>
        public Task StartAsync();
    }
}
