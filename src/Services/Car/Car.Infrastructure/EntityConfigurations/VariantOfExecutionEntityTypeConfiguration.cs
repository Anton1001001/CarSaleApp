using Car.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Car.Infrastructure.EntityConfigurations;

public class VariantOfExecutionEntityTypeConfiguration : IEntityTypeConfiguration<VariantOfExecution>
{
    public void Configure(EntityTypeBuilder<VariantOfExecution> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.HasIndex(e => e.BodyTypeId, "FK_VariantOfExecution_BodyType_IDX");

        builder.HasIndex(e => e.DriveTypeId, "FK_VariantOfExecution_DriveType_IDX");

        builder.HasIndex(e => e.EngineTypeId, "FK_VariantOfExecution_EngineType_IDX");

        builder.HasIndex(e => e.GenerationId, "FK_VariantOfExecution_Generation_IDX");

        builder.HasIndex(e => e.TransmissionTypeId, "FK_VariantOfExecution_TransmissionType_IDX");

        builder.HasOne(d => d.BodyType).WithMany(p => p.VariantsOfExecution)
            .HasForeignKey(d => d.BodyTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VariantOfExecution_BodyType");

        builder.HasOne(d => d.DriveType).WithMany(p => p.VariantsOfExecution)
            .HasForeignKey(d => d.DriveTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VariantOfExecution_DriveType");

        builder.HasOne(d => d.EngineType).WithMany(p => p.VariantsOfExecution)
            .HasForeignKey(d => d.EngineTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VariantOfExecution_EngineType");

        builder.HasOne(d => d.Generation).WithMany(p => p.VariantsOfExecution)
            .HasForeignKey(d => d.GenerationId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VariantOfExecution_Generation");

        builder.HasOne(d => d.TransmissionType).WithMany(p => p.VariantsOfExecution)
            .HasForeignKey(d => d.TransmissionTypeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("FK_VariantOfExecution_TransmissionType");
    }
}