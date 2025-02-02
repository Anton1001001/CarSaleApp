namespace Car.Infrastructure.EntityConfigurations;

public class CarGenerationEntityTypeConfiguration : IEntityTypeConfiguration<CarGeneration>
{
    public void Configure(EntityTypeBuilder<CarGeneration> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_generation", tb => tb.HasComment("Поколения Моделей"));

        builder.HasIndex(e => e.CarModelId, "id_car_model");

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_generation");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarModelId).HasColumnName("id_car_model");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.YearBegin)
            .HasMaxLength(255)
            .HasColumnName("year_begin");
        builder.Property(e => e.YearEnd)
            .HasMaxLength(255)
            .HasColumnName("year_end");

        builder.HasOne(d => d.CarModelNavigation).WithMany(p => p.CarGenerations)
            .HasForeignKey(d => d.CarModelId)
            .HasConstraintName("fk_car_generation_id_car_model");

        builder.HasOne(d => d.CarTypeNavigation).WithMany(p => p.CarGenerations)
            .HasForeignKey(d => d.CarTypeId)
            .HasConstraintName("fk_car_generation_id_car_type");
    }
}