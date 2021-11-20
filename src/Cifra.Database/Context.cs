using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cifra.Database
{
    public class Context : DbContext
    {
        public DbSet<Test> Tests { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreatedAsync();
        }
    }
}
