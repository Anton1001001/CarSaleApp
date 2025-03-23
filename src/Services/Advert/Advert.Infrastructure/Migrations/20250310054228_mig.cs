using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "parent_id",
                table: "place",
                type: "int",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int unsigned",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "place",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<int>(
                name: "place_region_id",
                table: "advert",
                type: "int",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int unsigned",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "place_country_id",
                table: "advert",
                type: "int",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int unsigned",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "place_city_id",
                table: "advert",
                type: "int",
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "int unsigned",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "parent_id",
                table: "place",
                type: "int unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "id",
                table: "place",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<uint>(
                name: "place_region_id",
                table: "advert",
                type: "int unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "place_country_id",
                table: "advert",
                type: "int unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "place_city_id",
                table: "advert",
                type: "int unsigned",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);
        }
    }
}
