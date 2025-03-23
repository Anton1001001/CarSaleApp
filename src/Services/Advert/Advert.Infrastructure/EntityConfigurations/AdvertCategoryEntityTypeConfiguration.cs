using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertCategoryEntityTypeConfiguration : IEntityTypeConfiguration<AdvertCategory>
{
    public void Configure(EntityTypeBuilder<AdvertCategory> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("advert_category");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.Label)
            .HasMaxLength(100)
            .HasColumnName("label");
        builder.Property(e => e.Name)
            .HasMaxLength(100)
            .HasColumnName("name");
    }
}