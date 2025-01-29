using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarDriveTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarDriveType>
{
    public void Configure(EntityTypeBuilder<CarDriveType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_drive_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_drive_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}