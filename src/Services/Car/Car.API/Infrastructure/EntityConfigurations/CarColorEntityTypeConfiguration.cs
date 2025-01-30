namespace Car.API.Infrastructure.EntityConfigurations;

public class CarColorEntityTypeConfiguration : IEntityTypeConfiguration<CarColor>
{
    public void Configure(EntityTypeBuilder<CarColor> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_color", tb => tb.HasComment("Цвета автомобилей"));

        builder.Property(e => e.Id)
            .HasComment("ID цвета")
            .HasColumnName("id_car_color");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название цвета")
            .HasColumnName("name");
    }
}