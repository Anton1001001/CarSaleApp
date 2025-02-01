namespace Car.API.Infrastructure.EntityConfigurations;

public class CarInteriorMaterialEntityTypeConfiguration : IEntityTypeConfiguration<CarInteriorMaterial>
{
    public void Configure(EntityTypeBuilder<CarInteriorMaterial> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_interior_material", tb => tb.HasComment("Материалы салона автомобилей"));

        builder.Property(e => e.Id)
            .HasComment("ID материала салона")
            .HasColumnName("id_car_interior_material");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasComment("Название материала салона")
            .HasColumnName("name");
    }
}