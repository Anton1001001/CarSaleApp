using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "refresh_available",
                table: "advert");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "refresh_available",
                table: "advert",
                type: "tinyint(1)",
                nullable: true);
        }
    }
}
