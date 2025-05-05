using Chat.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Chat.DataAccess.EntityConfigurations;

public class MessageEntityTypeConfiguration : IEntityTypeConfiguration<Message>
{
    public void Configure(EntityTypeBuilder<Message> builder)
    {
        builder.HasKey(m => m.Id);

        builder.Property(m => m.Text)
            .IsRequired()
            .HasMaxLength(2000);

        builder.Property(m => m.SenderId)
            .IsRequired();
        
        builder.Property(m => m.SentAt)
            .IsRequired();
        
        builder.HasOne(m => m.Dialog)
            .WithMany(d => d.Messages)
            .HasForeignKey(m => m.DialogId)
            .OnDelete(DeleteBehavior.Cascade);

    }
}