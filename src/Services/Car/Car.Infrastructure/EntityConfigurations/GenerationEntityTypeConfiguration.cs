using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class GenerationEntityTypeConfiguration : IEntityTypeConfiguration<Generation>
{
    public void Configure(EntityTypeBuilder<Generation> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");
        builder.HasIndex(e => e.ModelId, "FK_Generation_Model_IDX");
        builder.Property(e => e.Name).HasMaxLength(100);
        builder.HasOne(d => d.Model).WithMany(p => p.Generations)
            .HasForeignKey(d => d.ModelId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Generation_Model");
    }
}