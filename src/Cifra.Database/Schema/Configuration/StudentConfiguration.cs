using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

namespace Cifra.Database.Schema.Configuration
{
    public class StudentConfiguration : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            builder.ToTable("Students");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.FirstName)
                .HasMaxLength(100)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => Name.CreateFromString(x).Value
                );

            builder.Property(x => x.LastName)
                .HasMaxLength(100)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => Name.CreateFromString(x).Value
                );
        }
    }
}
