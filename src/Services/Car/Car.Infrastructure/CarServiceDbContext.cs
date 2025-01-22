using Car.Domain.Entities;
using Car.Infrastructure.EntityConfigurations;
using Microsoft.EntityFrameworkCore;
using DriveType = Car.Domain.Entities.DriveType;

namespace Car.Infrastructure;
public class CarServiceDbContext : DbContext
{
    public virtual DbSet<BodyType> BodyTypes { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<DriveType> DriveTypes { get; set; }

    public virtual DbSet<EngineType> EngineTypes { get; set; }

    public virtual DbSet<Generation> Generations { get; set; }

    public virtual DbSet<Model> Models { get; set; }

    public virtual DbSet<Modification> Modifications { get; set; }

    public virtual DbSet<TransmissionType> TransmissionTypes { get; set; }

    public virtual DbSet<VariantOfExecution> VariantsOfExecution { get; set; }

    public virtual DbSet<Vehicle> Vehicles { get; set; }
    
    public CarServiceDbContext()
    {
    }

    public CarServiceDbContext(DbContextOptions<CarServiceDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.ApplyConfiguration(new BodyTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new BrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new DriveTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EngineTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new GenerationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ModelEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ModificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new TransmissionTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VariantOfExecutionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new VehicleEntityTypeConfiguration());
    }
    
}
