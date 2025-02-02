namespace Car.Infrastructure.EntityConfigurations;

public class CarTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarType>
{
    public void Configure(EntityTypeBuilder<CarType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_type", tb => tb.HasComment("Автомобильный сайт"));

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}