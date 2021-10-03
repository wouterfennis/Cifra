using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cifra.Database
{
    public class Context : DbContext
    {
        private readonly IConnectionStringProvider _connectionStringProvider;

        public DbSet<Test> Tests { get; set; }

        public Context(IConnectionStringProvider connectionStringProvider)
        {
            _connectionStringProvider = connectionStringProvider;
            Database.EnsureCreatedAsync();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseMySQL(_connectionStringProvider.GetConnectionString());
        }
    }
}
