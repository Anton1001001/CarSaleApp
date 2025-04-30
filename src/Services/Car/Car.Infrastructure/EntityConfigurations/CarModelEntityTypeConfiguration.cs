namespace Car.Infrastructure.EntityConfigurations;

public class CarModelEntityTypeConfiguration : IEntityTypeConfiguration<CarModel>
{
    public void Configure(EntityTypeBuilder<CarModel> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_model", tb => tb.HasComment("Модели автомобилей"));

        builder.HasIndex(e => e.CarBrandId, "id_car_mark");

        builder.HasIndex(e => e.Name, "name");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasComment("ID")
            .HasColumnName("id");
        builder.Property(e => e.CarBrandId).HasColumnName("car_brand_id");
        builder.Property(e => e.Name).HasColumnName("name");
        builder.Property(e => e.Slug).HasMaxLength(255).HasColumnName("slug");

        builder.HasOne(d => d.CarBrand).WithMany(p => p.CarModels)
            .HasForeignKey(d => d.CarBrandId)
            .HasConstraintName("fk_car_model_id_car_mark");
    }
}