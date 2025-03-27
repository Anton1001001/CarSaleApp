using File.Core.Models;
using File.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace File.DataAccess;

public class FileDbContext : DbContext
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PhotoSize> PhotoSizes { get; set; }
    
    public FileDbContext()
    {
        
    }

    public FileDbContext(DbContextOptions<FileDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());

    }
}