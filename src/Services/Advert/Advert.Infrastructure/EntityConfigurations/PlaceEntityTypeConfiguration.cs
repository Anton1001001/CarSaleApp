using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class PlaceEntityTypeConfiguration : IEntityTypeConfiguration<Place>
{
    public void Configure(EntityTypeBuilder<Place> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("place");

        builder.HasIndex(e => e.ParentId, "parent_id");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.CaseLabel)
            .HasMaxLength(255)
            .HasColumnName("case_label");
        builder.Property(e => e.CaseLabelBel)
            .HasMaxLength(255)
            .HasColumnName("case_label_bel");
        builder.Property(e => e.Emoji)
            .HasMaxLength(10)
            .HasColumnName("emoji");
        builder.Property(e => e.Label)
            .HasMaxLength(255)
            .HasColumnName("label");
        builder.Property(e => e.LabelBel)
            .HasMaxLength(255)
            .HasColumnName("label_bel");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.ParentId).HasColumnName("parent_id");
        builder.Property(e => e.ShortName)
            .HasMaxLength(100)
            .HasColumnName("short_name");
        builder.Property(e => e.Type)
            .HasColumnType("enum('country','region','city')")
            .HasColumnName("type");

        builder.HasOne(d => d.Parent).WithMany(p => p.InverseParent)
            .HasForeignKey(d => d.ParentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}