using Car.API.Models;

namespace Car.API.Infrastructure.EntityConfigurations;

public class CarOptionEntityTypeConfiguration : IEntityTypeConfiguration<CarOption>
{
    public void Configure(EntityTypeBuilder<CarOption> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("car_option", tb => tb.HasComment("Опции"));

        builder.HasIndex(e => e.CarTypeId, "id_car_type");

        builder.Property(e => e.Id).HasColumnName("id_car_option");
        builder.Property(e => e.DateCreate).HasColumnName("date_create");
        builder.Property(e => e.DateUpdate).HasColumnName("date_update");
        builder.Property(e => e.CarTypeId).HasColumnName("id_car_type");
        builder.Property(e => e.ParentId).HasColumnName("id_parent");
        builder.Property(e => e.Name)
            .HasMaxLength(255)
            .HasColumnName("name");
    }
}