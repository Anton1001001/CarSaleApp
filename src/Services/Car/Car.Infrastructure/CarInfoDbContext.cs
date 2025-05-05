namespace Car.Infrastructure;

public class CarInfoDbContext : DbContext
{
    public CarInfoDbContext()
    {
    }

    public CarInfoDbContext(DbContextOptions<CarInfoDbContext> options)
        : base(options)
    {
    }

    public DbSet<CarBodyType> CarBodyTypes { get; set; }
    public DbSet<CarColor> CarColors { get; set; }
    public DbSet<CarCondition> CarConditions { get; set; }
    public DbSet<CarDriveType> CarDriveTypes { get; set; }
    public DbSet<CarEngineType> CarEngineTypes { get; set; }
    public DbSet<CarGeneration> CarGenerations { get; set; }
    public DbSet<CarInteriorColor> CarInteriorColors { get; set; }
    public DbSet<CarInteriorMaterial> CarInteriorMaterials { get; set; }
    public DbSet<CarBrand> CarBrands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<CarModification> CarModifications { get; set; }
    public DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");
        
        modelBuilder.ApplyConfiguration(new CarGenerationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarBrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarModelEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarModificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarColorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarConditionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarInteriorColorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarInteriorMaterialEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarBodyTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarDriveTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarEngineTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarTransmissionTypeEntityTypeConfiguration());
    }
}