namespace Cifra.Database
{
    /// <summary>
    /// Provider for a database connection string.
    /// </summary>
    public interface IConnectionStringProvider
    {
        /// <summary>
        /// Returns the connection string.
        /// </summary>
        public string GetConnectionString();
    }
}