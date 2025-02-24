using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertPrivateStatusEntityTypeConfiguration : IEntityTypeConfiguration<AdvertPrivateStatus>
{
    public void Configure(EntityTypeBuilder<AdvertPrivateStatus> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("advert_private_status");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.Label)
            .HasMaxLength(255)
            .HasColumnName("label");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.PhotoLabel)
            .HasMaxLength(255)
            .HasColumnName("photo_label");
        builder.Property(e => e.Published).HasColumnName("published");
    }
}