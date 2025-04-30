namespace Car.Infrastructure.EntityConfigurations;

public class CarBrandEntityTypeConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_brand", tb => tb.HasComment("Марки автомобилей"));

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasComment("ID")
            .HasColumnName("id");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.Slug)
            .HasMaxLength(255)
            .HasColumnName("slug");
    }
}