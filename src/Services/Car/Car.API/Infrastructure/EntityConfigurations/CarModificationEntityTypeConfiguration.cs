namespace Car.API.Infrastructure.EntityConfigurations;

public class CarModificationEntityTypeConfiguration : IEntityTypeConfiguration<CarModification>
{
    public void Configure(EntityTypeBuilder<CarModification> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("car_modification", tb => tb.HasComment("Модификации автомобилей"));

            builder.HasIndex(e => e.CarDriveTypeId, "id_car_drive_type");

            builder.HasIndex(e => e.CarEngineTypeId, "id_car_engine_type");

            builder.HasIndex(e => e.CarModelId, "id_car_model");

            builder.HasIndex(e => e.CarSerieId, "id_car_serie");

            builder.HasIndex(e => e.CarTransmissionTypeId, "id_car_transmission_type");

            builder.HasIndex(e => e.CarTypeId, "id_car_type");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("ID")
                .HasColumnName("id_car_modification");
            builder.Property(e => e.DateCreate).HasColumnName("date_create");
            builder.Property(e => e.DateUpdate).HasColumnName("date_update");
            builder.Property(e => e.EngineCapacity).HasColumnName("engine_capacity");
            builder.Property(e => e.EnginePower).HasColumnName("engine_power");
            builder.Property(e => e.FuelConsumptionCombined)
                .HasPrecision(5, 2)
                .HasColumnName("fuel_consumption_combined");
            builder.Property(e => e.GroundClearance)
                .HasPrecision(5, 2)
                .HasColumnName("ground_clearance");
            builder.Property(e => e.CarDriveTypeId).HasColumnName("id_car_drive_type");
            builder.Property(e => e.CarEngineTypeId).HasColumnName("id_car_engine_type");
            builder.Property(e => e.CarModelId).HasColumnName("id_car_model");
            builder.Property(e => e.CarSerieId).HasColumnName("id_car_serie");
            builder.Property(e => e.CarTransmissionTypeId).HasColumnName("id_car_transmission_type");
            builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            builder.HasOne(d => d.CarDriveTypeNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarDriveTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_drive_type");

            builder.HasOne(d => d.CarEngineTypeNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarEngineTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_engine_type");

            builder.HasOne(d => d.CarModelNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarModelId)
                .HasConstraintName("fk_car_model");

            builder.HasOne(d => d.CarSerieNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarSerieId)
                .HasConstraintName("fk_car_serie");

            builder.HasOne(d => d.CarTransmissionTypeNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarTransmissionTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_transmission_type");

            builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarTypeId)
                .HasConstraintName("fk_car_type");
    }
}