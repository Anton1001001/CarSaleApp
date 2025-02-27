using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Advert.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb3");

            migrationBuilder.CreateTable(
                name: "advert_private_status",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    published = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    photo_label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "advert_public_status",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "phone_code",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    label = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    emoji = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    code = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "place",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    parent_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    type = table.Column<string>(type: "enum('country','region','city')", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    short_name = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    label_bel = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    emoji = table.Column<string>(type: "varchar(10)", maxLength: 10, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    case_label = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    case_label_bel = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "FK_place_place_parent_id",
                        column: x => x.parent_id,
                        principalTable: "place",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "advert",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    advert_private_status_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    advert_public_status_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    AdvertStatus = table.Column<string>(type: "longtext", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    days_on_sale = table.Column<int>(type: "int", nullable: true),
                    description = table.Column<string>(type: "varchar(4000)", maxLength: 4000, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    version = table.Column<int>(type: "int", nullable: true),
                    published_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    refreshed_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    remove_reason = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    video_url = table.Column<string>(type: "varchar(1000)", maxLength: 1000, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    price_currency_id = table.Column<int>(type: "int", nullable: true),
                    price_amount = table.Column<int>(type: "int", nullable: true),
                    today_views = table.Column<int>(type: "int", nullable: true),
                    total_views = table.Column<int>(type: "int", nullable: true),
                    seller_name = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    total_phone_views = table.Column<int>(type: "int", nullable: true),
                    total_bookmarks = table.Column<int>(type: "int", nullable: true),
                    total_vin_views = table.Column<int>(type: "int", nullable: true),
                    next_refresh_available_at = table.Column<DateTime>(type: "datetime", nullable: true),
                    refresh_available = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    place_country_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    place_region_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    place_city_id = table.Column<uint>(type: "int unsigned", nullable: true),
                    properties = table.Column<string>(type: "json", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_advert_advert_private_status",
                        column: x => x.advert_private_status_id,
                        principalTable: "advert_private_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_advert_advert_public_status",
                        column: x => x.advert_public_status_id,
                        principalTable: "advert_public_status",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_advert_places1",
                        column: x => x.place_country_id,
                        principalTable: "place",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_advert_places2",
                        column: x => x.place_region_id,
                        principalTable: "place",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_advert_places3",
                        column: x => x.place_city_id,
                        principalTable: "place",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "advert_phone_number",
                columns: table => new
                {
                    id = table.Column<uint>(type: "int unsigned", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    advert_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    phone_code_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    number = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_advert_phone_number_advert1",
                        column: x => x.advert_id,
                        principalTable: "advert",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_advert_phone_number_phone_code1",
                        column: x => x.phone_code_id,
                        principalTable: "phone_code",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "advert_photo",
                columns: table => new
                {
                    advert_id = table.Column<uint>(type: "int unsigned", nullable: false),
                    file_id = table.Column<int>(type: "int", nullable: false),
                    is_main = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => new { x.advert_id, x.file_id })
                        .Annotation("MySql:IndexPrefixLength", new[] { 0, 0 });
                    table.ForeignKey(
                        name: "fk_advert_photo_advert1",
                        column: x => x.advert_id,
                        principalTable: "advert",
                        principalColumn: "id");
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_advert_advert_private_status_idx",
                table: "advert",
                column: "advert_private_status_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_advert_public_status1_idx",
                table: "advert",
                column: "advert_public_status_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_places1_idx",
                table: "advert",
                column: "place_country_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_places2_idx",
                table: "advert",
                column: "place_region_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_places3_idx",
                table: "advert",
                column: "place_city_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_phone_number_advert1_idx",
                table: "advert_phone_number",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_phone_number_phone_code1_idx",
                table: "advert_phone_number",
                column: "phone_code_id");

            migrationBuilder.CreateIndex(
                name: "fk_advert_photo_advert1_idx",
                table: "advert_photo",
                column: "advert_id");

            migrationBuilder.CreateIndex(
                name: "parent_id",
                table: "place",
                column: "parent_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "advert_phone_number");

            migrationBuilder.DropTable(
                name: "advert_photo");

            migrationBuilder.DropTable(
                name: "phone_code");

            migrationBuilder.DropTable(
                name: "advert");

            migrationBuilder.DropTable(
                name: "advert_private_status");

            migrationBuilder.DropTable(
                name: "advert_public_status");

            migrationBuilder.DropTable(
                name: "place");
        }
    }
}
