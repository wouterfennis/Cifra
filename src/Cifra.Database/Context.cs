using Cifra.Database.Converters;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;
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

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            configurationBuilder.Properties<Grade>().HaveConversion<GradeConverter>()
                .HaveColumnType("int");

            configurationBuilder.Properties<StandardizationFactor>().HaveConversion<StandardizationFactorConverter>()
                .HaveColumnType("int");

            configurationBuilder.Properties<Path>().HaveConversion<PathConverter>()
                .HaveColumnType("string");

            configurationBuilder.Properties<Name>().HaveConversion<NameConverter>()
                .HaveColumnType("string");
        }
    }
}
