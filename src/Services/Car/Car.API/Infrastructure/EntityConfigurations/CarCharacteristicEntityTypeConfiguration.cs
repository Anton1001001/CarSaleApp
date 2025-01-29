using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarCharacteristicEntityTypeConfiguration : IEntityTypeConfiguration<CarCharacteristic>
{
    public void Configure(EntityTypeBuilder<CarCharacteristic> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_characteristic", tb => tb.HasComment("Характеристики автомобилей"));

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.HasIndex(e => new { e.Name, IdParent = e.ParentId, IdCarType = e.CarTypeId }, "name");

        builder.Property(e => e.Id)
            .HasComment("id")
            .HasColumnName("id_car_characteristic");
        
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.ParentId).HasColumnName("id_parent");
        builder.Property(e => e.Name).HasColumnName("name");
    }
}