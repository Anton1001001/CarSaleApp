using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class EngineTypeEntityTypeConfiguration : IEntityTypeConfiguration<EngineType>
{
    public void Configure(EntityTypeBuilder<EngineType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}