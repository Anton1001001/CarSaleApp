using Chat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.EntityConfigurations;

public class DialogEntityTypeConfiguration : IEntityTypeConfiguration<Dialog>
{
    public void Configure(EntityTypeBuilder<Dialog> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.AdvertId)
            .IsRequired();

        builder.Property(d => d.Name)
            .HasMaxLength(50);

        builder.Property(d => d.SellerId)
            .IsRequired();

        builder.Property(d => d.BuyerId)
            .IsRequired();

        builder.Property(d => d.LastMessage)
            .HasMaxLength(1000);

        builder.Property(d => d.LastMessageTime)
            .IsRequired();

        builder.HasMany<Message>()
            .WithOne()
            .HasForeignKey(m => m.DialogId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}