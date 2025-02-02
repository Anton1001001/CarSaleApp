namespace Car.Infrastructure.EntityConfigurations;

public class CarExchangeOptionEntityTypeConfiguration : IEntityTypeConfiguration<CarExchangeOption>
{
    public void Configure(EntityTypeBuilder<CarExchangeOption> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_exchange_option", tb => tb.HasComment("Варианты обмена автомобилей"));

        builder.Property(e => e.Id)
            .HasComment("ID варианта обмена")
            .HasColumnName("id_car_exchange_option");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название варианта обмена")
            .HasColumnName("name");
    }
}