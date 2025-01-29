using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarEngineTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarEngineType>
{
    public void Configure(EntityTypeBuilder<CarEngineType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_engine_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_engine_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}