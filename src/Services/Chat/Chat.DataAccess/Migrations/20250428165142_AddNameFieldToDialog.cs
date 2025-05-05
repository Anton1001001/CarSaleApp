using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class AddNameFieldToDialog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Dialogs",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                collation: "utf8mb4_general_ci")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Dialogs");
        }
    }
}
