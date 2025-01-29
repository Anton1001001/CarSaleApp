using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarEquipmentEntityTypeConfiguration : IEntityTypeConfiguration<CarEquipment>
{
    public void Configure(EntityTypeBuilder<CarEquipment> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_equipment", tb => tb.HasComment("Комплектации"));

        builder.HasIndex(e => e.CarTypeId, "date_delete");

        builder.Property(e => e.Id)
            .HasComment("id")
            .HasColumnName("id_car_equipment");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarModificationId).HasColumnName("id_car_modification");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.PriceMin)
            .HasComment("Цена от")
            .HasColumnName("price_min");
        builder.Property(e => e.Year).HasColumnName("year");
    }
}