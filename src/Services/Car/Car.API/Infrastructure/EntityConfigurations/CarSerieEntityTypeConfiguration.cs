using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarSerieEntityTypeConfiguration : IEntityTypeConfiguration<CarSerie>
{
    public void Configure(EntityTypeBuilder<CarSerie> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_serie", tb => tb.HasComment("Cерии автомобилей"));

        builder.HasIndex(e => e.CarBodyTypeId, "id_car_body_type");

        builder.HasIndex(e => e.CarModelId, "id_car_model2");

        builder.HasIndex(e => e.CarTypeId, "id_car_type7");

        builder.Property(e => e.Id)
            .HasComment("ID")
            .HasColumnName("id_car_serie");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarBodyTypeId).HasColumnName("id_car_body_type");
        builder.Property(e => e.CarGenerationId).HasColumnName("id_car_generation");
        builder.Property(e => e.CarModelId).HasColumnName("id_car_model");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
    }
}