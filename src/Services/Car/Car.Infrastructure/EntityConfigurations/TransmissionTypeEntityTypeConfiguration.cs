using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class TransmissionTypeEntityTypeConfiguration : IEntityTypeConfiguration<TransmissionType>
{
    public void Configure(EntityTypeBuilder<TransmissionType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.Property(e => e.Name).HasMaxLength(100);
    }
}