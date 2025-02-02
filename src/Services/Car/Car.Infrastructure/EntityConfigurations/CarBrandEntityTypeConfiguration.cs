namespace Car.Infrastructure.EntityConfigurations;

public class CarBrandEntityTypeConfiguration : IEntityTypeConfiguration<CarBrand>
{
    public void Configure(EntityTypeBuilder<CarBrand> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_mark", tb => tb.HasComment("Марки автомобилей"));

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasComment("ID")
            .HasColumnName("id_car_mark");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.NameRus)
            .HasMaxLength(255)
            .HasColumnName("name_rus");

        builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarBrands)
            .HasForeignKey(d => d.CarTypeId)
            .HasConstraintName("fk_car_mark_id_car_type");
    }
}