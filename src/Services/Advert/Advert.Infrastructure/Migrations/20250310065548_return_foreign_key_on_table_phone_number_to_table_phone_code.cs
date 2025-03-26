using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class return_foreign_key_on_table_phone_number_to_table_phone_code : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "fk_advert_phone_number_phone_code1_idx",
                table: "advert_phone_number",
                column: "phone_code_id");

            migrationBuilder.AddForeignKey(
                name: "fk_advert_phone_number_phone_code1",
                table: "advert_phone_number",
                column: "phone_code_id",
                principalTable: "phone_code",
                principalColumn: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_advert_phone_number_phone_code1",
                table: "advert_phone_number");

            migrationBuilder.DropIndex(
                name: "fk_advert_phone_number_phone_code1_idx",
                table: "advert_phone_number");
        }
    }
}
