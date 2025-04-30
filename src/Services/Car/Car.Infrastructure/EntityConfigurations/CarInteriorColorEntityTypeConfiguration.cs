namespace Car.Infrastructure.EntityConfigurations;

public class CarInteriorColorEntityTypeConfiguration : IEntityTypeConfiguration<CarInteriorColor>
{
    public void Configure(EntityTypeBuilder<CarInteriorColor> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_interior_color", tb => tb.HasComment("Цвета салона автомобилей"));

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasComment("ID цвета салона")
            .HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название цвета салона")
            .HasColumnName("name");
    }
}