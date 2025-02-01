namespace Car.API.Infrastructure;

public class CarInfoDbContext : DbContext
{
    public CarInfoDbContext()
    {
    }

    public CarInfoDbContext(DbContextOptions<CarInfoDbContext> options)
        : base(options)
    {
    }
    public virtual DbSet<CarBodyType> CarBodyTypes { get; set; }
    public virtual DbSet<CarCharacteristic> CarCharacteristics { get; set; }
    public virtual DbSet<CarCharacteristicValue> CarCharacteristicValues { get; set; }
    public virtual DbSet<CarColor> CarColors { get; set; }
    public virtual DbSet<CarCondition> CarConditions { get; set; }
    public virtual DbSet<CarDriveType> CarDriveTypes { get; set; }
    public virtual DbSet<CarEngineType> CarEngineTypes { get; set; }
    public virtual DbSet<CarEquipment> CarEquipment { get; set; }
    public virtual DbSet<CarExchangeOption> CarExchangeOptions { get; set; }
    public virtual DbSet<CarGeneration> CarGenerations { get; set; }
    public virtual DbSet<CarInteriorColor> CarInteriorColors { get; set; }
    public virtual DbSet<CarInteriorMaterial> CarInteriorMaterials { get; set; }
    public virtual DbSet<CarBrand> CarBrands { get; set; }
    public virtual DbSet<CarModel> CarModels { get; set; }
    public virtual DbSet<CarModification> CarModifications { get; set; }
    public virtual DbSet<CarOption> CarOptions { get; set; }
    public virtual DbSet<CarOptionValue> CarOptionValues { get; set; }
    public virtual DbSet<CarSerie> CarSeries { get; set; }
    public virtual DbSet<CarTransmissionType> CarTransmissionTypes { get; set; }
    public virtual DbSet<CarType> CarTypes { get; set; }

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