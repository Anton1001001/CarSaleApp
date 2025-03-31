using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class make_video_url_not_required : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "video_url",
                table: "advert",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: true,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "advert",
                keyColumn: "video_url",
                keyValue: null,
                column: "video_url",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "video_url",
                table: "advert",
                type: "varchar(1000)",
                maxLength: 1000,
                nullable: false,
                collation: "utf8mb4_general_ci",
                oldClrType: typeof(string),
                oldType: "varchar(1000)",
                oldMaxLength: 1000,
                oldNullable: true)
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("Relational:Collation", "utf8mb4_general_ci");
        }
    }
}
