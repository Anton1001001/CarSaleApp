using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarBodyTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarBodyType>
{
    public void Configure(EntityTypeBuilder<CarBodyType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_body_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_body_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}