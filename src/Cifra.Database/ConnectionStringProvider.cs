using System;

namespace Cifra.Database
{
    /// <inheritdoc/>
    public class ConnectionStringProvider : IConnectionStringProvider
    {
        private readonly string _databaseConnectionString;

        public ConnectionStringProvider(string databaseConnectionString)
        {
            _databaseConnectionString = databaseConnectionString ?? throw new ArgumentNullException(nameof(databaseConnectionString));
        }

        /// <inheritdoc/>
        public string GetConnectionString()
        {
            return _databaseConnectionString;
        }
    }
}
