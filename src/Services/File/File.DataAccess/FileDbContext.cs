using File.Core.Models;
using File.DataAccess.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace File.DataAccess;

public class FileDbContext(DbContextOptions<FileDbContext> options) : DbContext(options)
{
    public DbSet<Photo> Photos { get; set; }
    public DbSet<PhotoSize> PhotoSizes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfiguration(new PhotoEntityTypeConfiguration());
    }
}