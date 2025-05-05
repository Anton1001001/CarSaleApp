using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car.Infrastructure.Migrations
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
                name: "car_body_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_brand",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    slug = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Марки автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_color",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID цвета"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название цвета", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Цвета автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_condition",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID состояния авто"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название состояния", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Состояние автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_drive_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_engine_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_interior_color",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID цвета салона"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название цвета салона", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Цвета салона автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_interior_material",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID материала салона"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название материала салона", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                },
                comment: "Материалы салона автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_transmission_type",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_model",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    slug = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    car_brand_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_model_id_car_mark",
                        column: x => x.car_brand_id,
                        principalTable: "car_brand",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Модели автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_generation",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    car_model_id = table.Column<int>(type: "int", nullable: false),
                    year_begin = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    year_end = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_generation_id_car_model",
                        column: x => x.car_model_id,
                        principalTable: "car_model",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Поколения Моделей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_modification",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    car_body_type_id = table.Column<int>(type: "int", nullable: false),
                    car_generation_id = table.Column<int>(type: "int", nullable: false),
                    car_engine_type_id = table.Column<int>(type: "int", nullable: true),
                    car_drive_type_id = table.Column<int>(type: "int", nullable: true),
                    car_transmission_type_id = table.Column<int>(type: "int", nullable: true),
                    engine_capacity = table.Column<int>(type: "int", nullable: true),
                    engine_power = table.Column<int>(type: "int", nullable: true),
                    fuel_consumption_combined = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    ground_clearance = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id);
                    table.ForeignKey(
                        name: "fk_car_drive_type",
                        column: x => x.car_drive_type_id,
                        principalTable: "car_drive_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_engine_type",
                        column: x => x.car_engine_type_id,
                        principalTable: "car_engine_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_modification_car_body_type1",
                        column: x => x.car_body_type_id,
                        principalTable: "car_body_type",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_modification_car_generation1",
                        column: x => x.car_generation_id,
                        principalTable: "car_generation",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_car_transmission_type",
                        column: x => x.car_transmission_type_id,
                        principalTable: "car_transmission_type",
                        principalColumn: "id",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "Модификации автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "id_car_model",
                table: "car_generation",
                column: "car_model_id");

            migrationBuilder.CreateIndex(
                name: "id_car_mark",
                table: "car_model",
                column: "car_brand_id");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "car_model",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "fk_car_modification_car_body_type1_idx",
                table: "car_modification",
                column: "car_body_type_id");

            migrationBuilder.CreateIndex(
                name: "fk_car_modification_car_generation1_idx",
                table: "car_modification",
                column: "car_generation_id");

            migrationBuilder.CreateIndex(
                name: "id_car_drive_type",
                table: "car_modification",
                column: "car_drive_type_id");

            migrationBuilder.CreateIndex(
                name: "id_car_engine_type",
                table: "car_modification",
                column: "car_engine_type_id");

            migrationBuilder.CreateIndex(
                name: "id_car_transmission_type",
                table: "car_modification",
                column: "car_transmission_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_color");

            migrationBuilder.DropTable(
                name: "car_condition");

            migrationBuilder.DropTable(
                name: "car_interior_color");

            migrationBuilder.DropTable(
                name: "car_interior_material");

            migrationBuilder.DropTable(
                name: "car_modification");

            migrationBuilder.DropTable(
                name: "car_drive_type");

            migrationBuilder.DropTable(
                name: "car_engine_type");

            migrationBuilder.DropTable(
                name: "car_body_type");

            migrationBuilder.DropTable(
                name: "car_generation");

            migrationBuilder.DropTable(
                name: "car_transmission_type");

            migrationBuilder.DropTable(
                name: "car_model");

            migrationBuilder.DropTable(
                name: "car_brand");
        }
    }
}
