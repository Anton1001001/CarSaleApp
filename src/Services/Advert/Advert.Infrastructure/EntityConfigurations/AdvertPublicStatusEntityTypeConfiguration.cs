using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertPublicStatusEntityTypeConfiguration : IEntityTypeConfiguration<AdvertPublicStatus>
{
    public void Configure(EntityTypeBuilder<AdvertPublicStatus> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("advert_public_status");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Label)
            .HasMaxLength(255)
            .HasColumnName("label");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}