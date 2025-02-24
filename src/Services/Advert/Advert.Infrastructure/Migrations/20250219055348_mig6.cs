using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class mig6 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "advert_id",
                table: "advert_photo",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "advert_id",
                table: "advert_phone_number",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned");

            migrationBuilder.AlterColumn<int>(
                name: "id",
                table: "advert",
                type: "int",
                nullable: false,
                oldClrType: typeof(uint),
                oldType: "int unsigned")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "advert_id",
                table: "advert_photo",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<uint>(
                name: "advert_id",
                table: "advert_phone_number",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<uint>(
                name: "id",
                table: "advert",
                type: "int unsigned",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);
        }
    }
}
