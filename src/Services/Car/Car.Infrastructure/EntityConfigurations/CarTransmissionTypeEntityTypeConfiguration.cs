namespace Car.Infrastructure.EntityConfigurations;

public class CarTransmissionTypeEntityTypeConfiguration : IEntityTypeConfiguration<CarTransmissionType>
{
    public void Configure(EntityTypeBuilder<CarTransmissionType> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_transmission_type");

        builder.Property(e => e.Id)
            .ValueGeneratedNever()
            .HasColumnName("id_car_transmission_type");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}