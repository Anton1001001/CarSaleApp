using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarModelEntityTypeConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_model", tb => tb.HasComment("Модели автомобилей"));

        builder.HasIndex(e => e.CarBrandId, "id_car_mark");

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.HasIndex(e => e.Name, "name");

        builder.Property(e => e.Id)
            .HasComment("ID")
            .HasColumnName("id_car_model");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarBrandId).HasColumnName("id_car_mark");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.NameRus)
            .HasMaxLength(255)
            .HasColumnName("name_rus");
    }
}