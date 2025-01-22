using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class ModelEntityTypeConfiguration : IEntityTypeConfiguration<Model>
{
    public void Configure(EntityTypeBuilder<Model> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.HasIndex(e => e.BrandId, "FK_Model_Brand_IDX");
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.HasOne(d => d.Brand).WithMany(p => p.Models)
            .HasForeignKey(d => d.BrandId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Model_Brand");
    }
}