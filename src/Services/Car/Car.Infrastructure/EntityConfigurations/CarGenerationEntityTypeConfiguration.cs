namespace Car.Infrastructure.EntityConfigurations;

public class CarGenerationEntityTypeConfiguration : IEntityTypeConfiguration<CarGeneration>
{
    public void Configure(EntityTypeBuilder<CarGeneration> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_generation");

        builder.HasIndex(e => e.CarModelId, "id_car_model");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id");
        builder.Property(e => e.CarModelId).HasColumnName("car_model_id");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
        builder.Property(e => e.YearBegin)
            .HasColumnName("year_begin");
        builder.Property(e => e.YearEnd)
            .HasColumnName("year_end");

        builder.HasOne(d => d.CarModel).WithMany(p => p.CarGenerations)
            .HasForeignKey(d => d.CarModelId)
            .HasConstraintName("fk_car_generation_id_car_model");
    }
}