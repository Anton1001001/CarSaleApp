using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class VehicleEntityTypeConfiguration : IEntityTypeConfiguration<Vehicle>
{
    public void Configure(EntityTypeBuilder<Vehicle> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.ModificationId, "FK_Vehicle_Modification_IDX");

        builder.HasIndex(e => e.VariantOfExecutionId, "FK_Vehicle_VariantOfExecution_IDX");

        builder.HasOne(d => d.Modification).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.ModificationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Vehicle_Modification");

        builder.HasOne(d => d.VariantOfExecution).WithMany(p => p.Vehicles)
            .HasForeignKey(d => d.VariantOfExecutionId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_Vehicle_VariantOfExecution");
    }
}