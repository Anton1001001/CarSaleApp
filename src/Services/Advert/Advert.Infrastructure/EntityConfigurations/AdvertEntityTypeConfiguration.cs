using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Advert.Infrastructure.EntityConfigurations;

public class AdvertEntityTypeConfiguration : IEntityTypeConfiguration<Domain.Entities.Advert>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Advert> builder)
    {
        builder.HasKey(e => e.Id).HasName("PRIMARY");

        builder.ToTable("advert");

        builder.HasIndex(e => e.AdvertPrivateStatusId, "fk_advert_advert_private_status_idx");

        builder.HasIndex(e => e.AdvertPublicStatusId, "fk_advert_advert_public_status1_idx");

        builder.HasIndex(e => e.PlaceCountryId, "fk_advert_places1_idx");

        builder.HasIndex(e => e.PlaceRegionId, "fk_advert_places2_idx");

        builder.HasIndex(e => e.PlaceCityId, "fk_advert_places3_idx");

        builder.Property(e => e.Id).HasColumnName("id");
        builder.Property(e => e.AdvertStatus).HasColumnName("advert_status");
        builder.Property(e => e.AdvertPrivateStatusId).HasColumnName("advert_private_status_id");
        builder.Property(e => e.AdvertPublicStatusId).HasColumnName("advert_public_status_id");
        builder.Property(e => e.DaysOnSale).HasColumnName("days_on_sale");
        builder.Property(e => e.Description)
            .HasMaxLength(4000)
            .HasColumnName("description");
        builder.Property(e => e.NextRefreshAvailableAt)
            .HasColumnType("datetime")
            .HasColumnName("next_refresh_available_at");
        builder.Property(e => e.PlaceCityId).HasColumnName("place_city_id");
        builder.Property(e => e.PlaceCountryId).HasColumnName("place_country_id");
        builder.Property(e => e.PlaceRegionId).HasColumnName("place_region_id");
        builder.Property(e => e.PriceAmount).HasColumnName("price_amount");
        builder.Property(a => a.PriceCurrency)
            .HasConversion<int>()
            .IsRequired(false)
            .HasColumnName("price_currency");
        builder.Property(e => e.Properties)
            .HasColumnType("json")
            .HasColumnName("properties");
        builder.Property(e => e.PublishedAt)
            .HasColumnType("datetime")
            .HasColumnName("published_at");
        builder.Property(e => e.RefreshedAt)
            .HasColumnType("datetime")
            .HasColumnName("refreshed_at");
        builder.Property(e => e.RemoveReason)
            .HasMaxLength(20)
            .HasColumnName("remove_reason");
        builder.Property(e => e.SellerName)
            .HasMaxLength(25)
            .HasColumnName("seller_name");
        builder.Property(e => e.TodayViews).HasColumnName("today_views");
        builder.Property(e => e.TotalBookmarks).HasColumnName("total_bookmarks");
        builder.Property(e => e.TotalPhoneViews).HasColumnName("total_phone_views");
        builder.Property(e => e.TotalViews).HasColumnName("total_views");
        builder.Property(e => e.TotalVinViews).HasColumnName("total_vin_views");
        builder.Property(e => e.Version).HasColumnName("version");
        builder.Property(e => e.VideoUrl)
            .HasMaxLength(1000)
            .HasColumnName("video_url");

        builder.HasOne(d => d.AdvertPrivateStatus).WithMany(p => p.Adverts)
            .HasForeignKey(d => d.AdvertPrivateStatusId)
            .HasConstraintName("fk_advert_advert_private_status");

        builder.HasOne(d => d.AdvertPublicStatus).WithMany(p => p.Adverts)
            .HasForeignKey(d => d.AdvertPublicStatusId)
            .HasConstraintName("fk_advert_advert_public_status");

        builder.HasOne(d => d.PlaceCity).WithMany(p => p.AdvertPlaceCities)
            .HasForeignKey(d => d.PlaceCityId)
            .HasConstraintName("fk_advert_places3");

        builder.HasOne(d => d.PlaceCountry).WithMany(p => p.AdvertPlaceCountries)
            .HasForeignKey(d => d.PlaceCountryId)
            .HasConstraintName("fk_advert_places1");

        builder.HasOne(d => d.PlaceRegion).WithMany(p => p.AdvertPlaceRegions)
            .HasForeignKey(d => d.PlaceRegionId)
            .HasConstraintName("fk_advert_places2");
    }
}