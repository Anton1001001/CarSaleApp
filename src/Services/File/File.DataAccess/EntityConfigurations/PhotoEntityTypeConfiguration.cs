using File.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace File.DataAccess.EntityConfigurations;

public class PhotoEntityTypeConfiguration : IEntityTypeConfiguration<Photo>
{
    public void Configure(EntityTypeBuilder<Photo> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(p => p.Id).ValueGeneratedOnAdd();

        builder.OwnsOne(p => p.Big);
        builder.OwnsOne(p => p.Medium);
        builder.OwnsOne(p => p.Small);
        builder.OwnsOne(p => p.ExtraSmall);
    }
}