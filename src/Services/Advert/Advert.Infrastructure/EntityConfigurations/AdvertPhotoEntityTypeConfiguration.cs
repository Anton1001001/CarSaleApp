using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertPhotoEntityTypeConfiguration : IEntityTypeConfiguration<AdvertPhoto>
{
    public void Configure(EntityTypeBuilder<AdvertPhoto> builder)
    {
        builder.HasKey(e => new { e.AdvertId, e.FileId })
            .HasName("PRIMARY")
            .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

        builder.ToTable("advert_photo");

        builder.HasIndex(e => e.AdvertId, "fk_advert_photo_advert1_idx");

        builder.Property(e => e.AdvertId).HasColumnName("advert_id");
        builder.Property(e => e.FileId).HasColumnName("file_id");
        builder.Property(e => e.IsMain).HasColumnName("is_main");

        builder.HasOne(d => d.Advert).WithMany(p => p.AdvertPhotos)
            .HasForeignKey(d => d.AdvertId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_advert_photo_advert1");
    }
}