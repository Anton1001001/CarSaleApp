namespace Car.Infrastructure.EntityConfigurations;

public class CarModificationEntityTypeConfiguration : IEntityTypeConfiguration<CarModification>
{
    public void Configure(EntityTypeBuilder<CarModification> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("car_modification", tb => tb.HasComment("Модификации автомобилей"));

            builder.HasIndex(e => e.CarBodyTypeId, "fk_car_modification_car_body_type1_idx");

            builder.HasIndex(e => e.CarGenerationId, "fk_car_modification_car_generation1_idx");

            builder.HasIndex(e => e.CarDriveTypeId, "id_car_drive_type");

            builder.HasIndex(e => e.CarEngineTypeId, "id_car_engine_type");

            builder.HasIndex(e => e.CarTransmissionTypeId, "id_car_transmission_type");

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasComment("ID")
                .HasColumnName("id");
            builder.Property(e => e.CarBodyTypeId).HasColumnName("car_body_type_id");
            builder.Property(e => e.CarDriveTypeId).HasColumnName("car_drive_type_id");
            builder.Property(e => e.CarEngineTypeId).HasColumnName("car_engine_type_id");
            builder.Property(e => e.CarGenerationId).HasColumnName("car_generation_id");
            builder.Property(e => e.CarTransmissionTypeId).HasColumnName("car_transmission_type_id");
            builder.Property(e => e.EngineCapacity).HasColumnName("engine_capacity");
            builder.Property(e => e.EnginePower).HasColumnName("engine_power");
            builder.Property(e => e.FuelConsumptionCombined)
                .HasPrecision(5, 2)
                .HasColumnName("fuel_consumption_combined");
            builder.Property(e => e.GroundClearance)
                .HasPrecision(5, 2)
                .HasColumnName("ground_clearance");
            builder.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");

            builder.HasOne(d => d.CarBodyType).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarBodyTypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_car_modification_car_body_type1");

            builder.HasOne(d => d.CarDriveType).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarDriveTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_drive_type");

            builder.HasOne(d => d.CarEngineType).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarEngineTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_engine_type");

            builder.HasOne(d => d.CarGeneration).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarGenerationId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_car_modification_car_generation1");

            builder.HasOne(d => d.CarTransmissionType).WithMany(p => p.CarModifications)
                .HasForeignKey(d => d.CarTransmissionTypeId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("fk_car_transmission_type");
    }
}