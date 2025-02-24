using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price_currency_id",
                table: "advert",
                newName: "PriceCurrency");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PriceCurrency",
                table: "advert",
                newName: "price_currency_id");
        }
    }
}
