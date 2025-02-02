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
    public DbSet<CarCharacteristic> CarCharacteristics { get; set; }
    public DbSet<CarCharacteristicValue> CarCharacteristicValues { get; set; }
    public DbSet<CarColor> CarColors { get; set; }
    public DbSet<CarCondition> CarConditions { get; set; }
    public DbSet<CarDriveType> CarDriveTypes { get; set; }
    public DbSet<CarEngineType> CarEngineTypes { get; set; }
    public DbSet<CarEquipment> CarEquipment { get; set; }
    public DbSet<CarExchangeOption> CarExchangeOptions { get; set; }
    public DbSet<CarGeneration> CarGenerations { get; set; }
    public DbSet<CarInteriorColor> CarInteriorColors { get; set; }
    public DbSet<CarInteriorMaterial> CarInteriorMaterials { get; set; }
    public DbSet<CarBrand> CarBrands { get; set; }
    public DbSet<CarModel> CarModels { get; set; }
    public DbSet<CarModification> CarModifications { get; set; }
    public DbSet<CarOption> CarOptions { get; set; }
    public DbSet<CarOptionValue> CarOptionValues { get; set; }
    public DbSet<CarSerie> CarSeries { get; set; }
    public DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }
    public DbSet<CarType> CarTypes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb3_general_ci")
            .HasCharSet("utf8mb3");

        modelBuilder.ApplyConfiguration(new CarCharacteristicEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarCharacteristicValueEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarEquipmentEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarGenerationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarBrandEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarModelEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarModificationEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarOptionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarOptionValueEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarSerieEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarColorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarConditionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarExchangeOptionEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarInteriorColorEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarInteriorMaterialEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarBodyTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarDriveTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarEngineTypeEntityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new CarTransmissionTypeEntityTypeConfiguration());
    }
}