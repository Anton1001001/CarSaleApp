using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class BodyTypeEntityTypeConfiguration : IEntityTypeConfiguration<BodyType>
{
    public void Configure(EntityTypeBuilder<BodyType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.Property(e => e.Name).HasMaxLength(50);
    }
}