namespace Car.Infrastructure.EntityConfigurations;

public class CarConditionEntityTypeConfiguration : IEntityTypeConfiguration<CarCondition>
{
    public void Configure(EntityTypeBuilder<CarCondition> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_condition", tb => tb.HasComment("Состояние автомобилей"));

        builder.Property(e => e.Id)
            .HasComment("ID состояния авто")
            .HasColumnName("id_car_condition");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название состояния")
            .HasColumnName("name");
    }
}