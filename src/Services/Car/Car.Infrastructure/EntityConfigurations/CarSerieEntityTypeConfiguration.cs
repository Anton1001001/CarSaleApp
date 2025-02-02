namespace Car.Infrastructure.EntityConfigurations;

public class CarSerieEntityTypeConfiguration : IEntityTypeConfiguration<CarSerie>
{
    public void Configure(EntityTypeBuilder<CarSerie> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_serie", tb => tb.HasComment("Cерии автомобилей"));

        builder.HasIndex(e => e.CarGenerationId, "fk_car_generation_serie");

        builder.HasIndex(e => e.CarBodyTypeId, "id_car_body_type");

        builder.HasIndex(e => e.CarModelId, "id_car_model");

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasComment("ID")
            .HasColumnName("id_car_serie");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarBodyTypeId).HasColumnName("id_car_body_type");
        builder.Property(e => e.CarGenerationId).HasColumnName("id_car_generation");
        builder.Property(e => e.CarModelId).HasColumnName("id_car_model");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");

        builder.HasOne(d => d.CarBodyTypeNavigation).WithMany(p => p.CarSeries)
            .HasForeignKey(d => d.CarBodyTypeId)
            .HasConstraintName("fk_car_body_type_serie");

        builder.HasOne(d => d.CarGenerationNavigation).WithMany(p => p.CarSeries)
            .HasForeignKey(d => d.CarGenerationId)
            .OnDelete(DeleteBehavior.SetNull)
            .HasConstraintName("fk_car_generation_serie");

        builder.HasOne(d => d.CarModelNavigation).WithMany(p => p.CarSeries)
            .HasForeignKey(d => d.CarModelId)
            .HasConstraintName("fk_car_model_serie");

        builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarSeries)
            .HasForeignKey(d => d.CarTypeId)
            .HasConstraintName("fk_car_type_serie");
    }
}