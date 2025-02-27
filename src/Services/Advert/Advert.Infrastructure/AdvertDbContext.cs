using Advert.Domain.Entities;
using Advert.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;

namespace Advert.Infrastructure;

public class AdvertDbContext : DbContext
{
    public DbSet<Domain.Entities.Advert> Adverts { get; set; }
    public DbSet<AdvertPhoneNumber> AdvertPhoneNumbers { get; set; }
    public DbSet<AdvertPhoto> AdvertPhotos { get; set; }
    public DbSet<AdvertPrivateStatus?> AdvertPrivateStatuses { get; set; }
    public DbSet<AdvertPublicStatus> AdvertPublicStatuses { get; set; }
    public DbSet<PhoneCode> PhoneCodes { get; set; }
    public DbSet<Place> Places { get; set; }

    public AdvertDbContext()
    {
    }

    public AdvertDbContext(DbContextOptions<AdvertDbContext> options)
        : base(options)
    {
    }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.ApplyConfiguration(new AdvertEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertPhoneNumberEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertPhotoEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertPrivateStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new AdvertPublicStatusEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PhoneCodeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new PlaceEntityTypeConfiguration());
    }
}