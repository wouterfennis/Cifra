using Cifra.Database.Schema;
using Microsoft.EntityFrameworkCore;

namespace Cifra.Database
{
    public class Context : DbContext
    {
        public DbSet<Test> Tests { get; set; }
        public DbSet<Assignment> Assignments { get; set; }
        public DbSet<Class> Classes { get; set; }
        public DbSet<Student> Students { get; set; }

        public Context(DbContextOptions options) : base(options)
        {
            Database.EnsureCreated();
        }
    }
}
