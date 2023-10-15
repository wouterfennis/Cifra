using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

namespace Cifra.Database.Schema.Configuration
{
    public class TestConfiguration : IEntityTypeConfiguration<Test>
    {
        public void Configure(EntityTypeBuilder<Test> builder)
        {
            builder.ToTable("Tests");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired()
                .HasConversion(
                    x => x.Value,
                    x => Name.CreateFromString(x).Value
                );

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder.Property(x => x.StandardizationFactor)
                .HasConversion(
                    x => x.Value,
                    x => StandardizationFactor.CreateFromInteger(x).Value
                );

            builder.Property(x => x.MinimumGrade)
                .HasConversion(
                    x => x.Value,
                    x => Grade.CreateFromInteger(x).Value
                );

            builder
                .HasMany(x => x.Assignments);
        }
    }
}
