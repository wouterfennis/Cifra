using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using Cifra.Domain;
using Cifra.Domain.ValueTypes;

namespace Cifra.Database.Schema.Configuration
{
    public class ClassConfiguration : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            builder.ToTable("Classes");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Name)
                .HasMaxLength(100)
                .IsRequired();
                //.HasConversion(
                //    x => x.Value,
                //    x => Name.CreateFromString(x).Value
                //);

            builder.HasIndex(x => x.Name)
                .IsUnique();

            builder
                .HasMany(x => x.Students);
        }
    }
}
