using Chat.Core.Entities;
using Chat.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Chat.DataAccess;

public class ChatDbContext(DbContextOptions<ChatDbContext> options) : DbContext(options)
{
    public DbSet<Message> Messages { get; set; }
    public DbSet<Dialog> Dialogs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");
        
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new DialogEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new MessageEntityTypeConfiguration());
    }
}