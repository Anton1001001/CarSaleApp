namespace Car.API.Infrastructure.EntityConfigurations;

public class CarInteriorColorEntityTypeConfiguration : IEntityTypeConfiguration<CarInteriorColor>
{
    public void Configure(EntityTypeBuilder<CarInteriorColor> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_interior_color", tb => tb.HasComment("Цвета салона автомобилей"));

        builder.Property(e => e.Id)
            .HasComment("ID цвета салона")
            .HasColumnName("id_car_interior_color");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название цвета салона")
            .HasColumnName("name");
    }
}