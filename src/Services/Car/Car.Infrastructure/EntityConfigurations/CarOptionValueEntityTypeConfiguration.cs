namespace Car.Infrastructure.EntityConfigurations;

public class CarOptionValueEntityTypeConfiguration : IEntityTypeConfiguration<CarOptionValue>
{
    public void Configure(EntityTypeBuilder<CarOptionValue> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_option_value", tb => tb.HasComment("Значения опций"));

        builder.HasIndex(e => e.CarTypeId, "date_delete");

        builder.HasIndex(e => e.CarEquipmentId, "fk_car_option_value_equipment");

        builder.HasIndex(
            e => new { IdCarOption = e.CarOptionId, IdCarEquipment = e.CarEquipmentId, IdCarType = e.CarTypeId },
            "id_car_option").IsUnique();

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_option_value");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarEquipmentId).HasColumnName("id_car_equipment");
        builder.Property(e => e.CarOptionId).HasColumnName("id_car_option");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.IsBase)
            .IsRequired()
            .HasDefaultValueSql("'1'")
            .HasColumnName("is_base");

        builder.HasOne(d => d.CarEquipmentNavigation).WithMany(p => p.CarOptionValues)
            .HasForeignKey(d => d.CarEquipmentId)
            .HasConstraintName("fk_car_option_value_equipment");

        builder.HasOne(d => d.CarOptionNavigation).WithMany(p => p.CarOptionValues)
            .HasForeignKey(d => d.CarOptionId)
            .HasConstraintName("fk_car_option_value_option");

        builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarOptionValues)
            .HasForeignKey(d => d.CarTypeId)
            .HasConstraintName("fk_car_option_value_type");
    }
}