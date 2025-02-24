using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceCurrency",
                table: "advert",
                newName: "price_currency");

            migrationBuilder.RenameColumn(
                name: "AdvertStatus",
                table: "advert",
                newName: "advert_status");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_currency",
                table: "advert",
                newName: "PriceCurrency");

            migrationBuilder.RenameColumn(
                name: "advert_status",
                table: "advert",
                newName: "AdvertStatus");
        }
    }
}
