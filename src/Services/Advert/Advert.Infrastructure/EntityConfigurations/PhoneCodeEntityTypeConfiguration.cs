using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class PhoneCodeEntityTypeConfiguration : IEntityTypeConfiguration<PhoneCode>
{
    public void Configure(EntityTypeBuilder<PhoneCode> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("phone_code");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Code)
            .HasMaxLength(45)
            .HasColumnName("code");
        builder.Property(e => e.Emoji)
            .HasMaxLength(45)
            .HasColumnName("emoji");
        builder.Property(e => e.Label)
            .HasMaxLength(45)
            .HasColumnName("label");
    }
}