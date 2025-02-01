namespace Car.API.Infrastructure.EntityConfigurations;

public class CarCharacteristicValueEntityTypeConfiguration : IEntityTypeConfiguration<CarCharacteristicValue>
{
    public void Configure(EntityTypeBuilder<CarCharacteristicValue> builder)
    {
            builder.HasKey(e => e.Id).HasName("PRIMARY");

            builder.ToTable("car_characteristic_value", tb => tb.HasComment("Значения характеристик автомобиля"));

            builder.HasIndex(e => e.CarModificationId, "fk_car_characteristic_value_id_car_modification");

            builder.HasIndex(e => new { IdCarCharacteristic = e.CarCharacteristicId, IdCarModification = e.CarModificationId, IdCarType = e.CarTypeId }, "id_car_characteristic").IsUnique();

            builder.HasIndex(e => e.CarTypeId, "id_car_type");

            builder.HasIndex(e => new { IdCarCharacteristic = e.CarCharacteristicId, IdCarModification = e.CarModificationId }, "id_characteristic").IsUnique();

            builder.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id_car_characteristic_value");
            builder.Property(e => e.DateCreate).HasColumnName("date_create");
            builder.Property(e => e.DateUpdate).HasColumnName("date_update");
            builder.Property(e => e.CarCharacteristicId).HasColumnName("id_car_characteristic");
            builder.Property(e => e.CarModificationId).HasColumnName("id_car_modification");
            builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
            builder.Property(e => e.Unit)
                .HasMaxLength(255)
                .HasComment("Еденица измерения")
                .HasColumnName("unit");
            builder.Property(e => e.Value)
                .HasMaxLength(255)
                .HasColumnName("value");

            builder.HasOne(d => d.CarCharacteristicNavigation).WithMany(p => p.CarCharacteristicValues)
                .HasForeignKey(d => d.CarCharacteristicId)
                .HasConstraintName("fk_car_characteristic_value_id_car_characteristic");

            builder.HasOne(d => d.CarModificationNavigation).WithMany(p => p.CarCharacteristicValues)
                .HasForeignKey(d => d.CarModificationId)
                .HasConstraintName("fk_car_characteristic_value_id_car_modification");

            builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarCharacteristicValues)
                .HasForeignKey(d => d.CarTypeId)
                .HasConstraintName("fk_car_characteristic_value_id_car_type");
    }
}