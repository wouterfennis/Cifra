using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cifra.Database
{
    internal class Context : DbContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        DbSet<Test> Tests { get; set; }

        public Context(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(_connectionStringProvider.GetConnectionString());
        }
    }
}
