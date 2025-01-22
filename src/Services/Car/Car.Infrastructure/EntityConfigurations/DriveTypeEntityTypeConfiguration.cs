using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DriveType = Car.Domain.Entities.DriveType;

namespace Car.Infrastructure.EntityConfigurations;

public class DriveTypeEntityTypeConfiguration : IEntityTypeConfiguration<DriveType>
{
    public void Configure(EntityTypeBuilder<DriveType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.Property(e => e.Name).HasMaxLength(100);
    }
}