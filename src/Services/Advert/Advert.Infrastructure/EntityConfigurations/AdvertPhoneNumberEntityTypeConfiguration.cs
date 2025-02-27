using Advert.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertPhoneNumberEntityTypeConfiguration : IEntityTypeConfiguration<AdvertPhoneNumber>
{
    public void Configure(EntityTypeBuilder<AdvertPhoneNumber> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("advert_phone_number");

        builder.HasIndex(e => e.AdvertId, "fk_advert_phone_number_advert1_idx");

        builder.HasIndex(e => e.PhoneCodeId, "fk_advert_phone_number_phone_code1_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AdvertId).HasColumnName("advert_id");
        builder.Property(e => e.Number)
            .HasMaxLength(20)
            .HasColumnName("number");
        builder.Property(e => e.PhoneCodeId).HasColumnName("phone_code_id");

        builder.HasOne(d => d.Advert).WithMany(p => p.AdvertPhoneNumbers)
            .HasForeignKey(d => d.AdvertId)
            .OnDelete(DeleteBehavior.ClientCascade)
            .HasConstraintName("fk_advert_phone_number_advert1");

        builder.HasOne(d => d.PhoneCode).WithMany(p => p.AdvertPhoneNumbers)
            .HasForeignKey(d => d.PhoneCodeId)
            .OnDelete(DeleteBehavior.ClientSetNull)
            .HasConstraintName("fk_advert_phone_number_phone_code1");
    }
}