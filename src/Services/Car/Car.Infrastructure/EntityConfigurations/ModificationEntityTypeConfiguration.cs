using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class ModificationEntityTypeConfiguration : IEntityTypeConfiguration<Modification>
{
    public void Configure(EntityTypeBuilder<Modification> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.Property(e => e.Name).HasMaxLength(70);
    }
}