using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Car.API.Migrations
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
                    id_car_body_type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_body_type);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_color",
                columns: table => new
                {
                    id_car_color = table.Column<int>(type: "int", nullable: false, comment: "ID цвета")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название цвета", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_color);
                },
                comment: "Цвета автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_condition",
                columns: table => new
                {
                    id_car_condition = table.Column<int>(type: "int", nullable: false, comment: "ID состояния авто")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название состояния", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_condition);
                },
                comment: "Состояние автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_drive_type",
                columns: table => new
                {
                    id_car_drive_type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_drive_type);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_engine_type",
                columns: table => new
                {
                    id_car_engine_type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_engine_type);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_exchange_option",
                columns: table => new
                {
                    id_car_exchange_option = table.Column<int>(type: "int", nullable: false, comment: "ID варианта обмена")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название варианта обмена", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_exchange_option);
                },
                comment: "Варианты обмена автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_interior_color",
                columns: table => new
                {
                    id_car_interior_color = table.Column<int>(type: "int", nullable: false, comment: "ID цвета салона")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название цвета салона", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_interior_color);
                },
                comment: "Цвета салона автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_interior_material",
                columns: table => new
                {
                    id_car_interior_material = table.Column<int>(type: "int", nullable: false, comment: "ID материала салона")
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, comment: "Название материала салона", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_interior_material);
                },
                comment: "Материалы салона автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_transmission_type",
                columns: table => new
                {
                    id_car_transmission_type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_transmission_type);
                })
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_type",
                columns: table => new
                {
                    id_car_type = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_type);
                },
                comment: "Автомобильный сайт")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_characteristic",
                columns: table => new
                {
                    id_car_characteristic = table.Column<int>(type: "int", nullable: false, comment: "id"),
                    name = table.Column<string>(type: "varchar(255)", nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_parent = table.Column<int>(type: "int", nullable: true),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_characteristic);
                    table.ForeignKey(
                        name: "fk_car_characteristic_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_characteristic_id_parent",
                        column: x => x.id_parent,
                        principalTable: "car_characteristic",
                        principalColumn: "id_car_characteristic",
                        onDelete: ReferentialAction.SetNull);
                },
                comment: "Характеристики автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_mark",
                columns: table => new
                {
                    id_car_mark = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false),
                    name_rus = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_mark);
                    table.ForeignKey(
                        name: "fk_car_mark_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Марки автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_option",
                columns: table => new
                {
                    id_car_option = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_parent = table.Column<int>(type: "int", nullable: true),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: false),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: false),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_option);
                    table.ForeignKey(
                        name: "fk_car_option_parent",
                        column: x => x.id_parent,
                        principalTable: "car_option",
                        principalColumn: "id_car_option",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_type_option",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Опции")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_model",
                columns: table => new
                {
                    id_car_model = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    id_car_mark = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false),
                    name_rus = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_model);
                    table.ForeignKey(
                        name: "fk_car_model_id_car_mark",
                        column: x => x.id_car_mark,
                        principalTable: "car_mark",
                        principalColumn: "id_car_mark",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_model_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Модели автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_generation",
                columns: table => new
                {
                    id_car_generation = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_car_model = table.Column<int>(type: "int", nullable: false),
                    year_begin = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    year_end = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_generation);
                    table.ForeignKey(
                        name: "fk_car_generation_id_car_model",
                        column: x => x.id_car_model,
                        principalTable: "car_model",
                        principalColumn: "id_car_model",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_generation_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Поколения Моделей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_serie",
                columns: table => new
                {
                    id_car_serie = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    id_car_model = table.Column<int>(type: "int", nullable: false),
                    id_car_body_type = table.Column<int>(type: "int", nullable: false),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_generation = table.Column<int>(type: "int", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_serie);
                    table.ForeignKey(
                        name: "fk_car_body_type_serie",
                        column: x => x.id_car_body_type,
                        principalTable: "car_body_type",
                        principalColumn: "id_car_body_type",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_generation_serie",
                        column: x => x.id_car_generation,
                        principalTable: "car_generation",
                        principalColumn: "id_car_generation",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_model_serie",
                        column: x => x.id_car_model,
                        principalTable: "car_model",
                        principalColumn: "id_car_model",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_type_serie",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Cерии автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_modification",
                columns: table => new
                {
                    id_car_modification = table.Column<int>(type: "int", nullable: false, comment: "ID"),
                    id_car_serie = table.Column<int>(type: "int", nullable: false),
                    id_car_model = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false),
                    id_car_engine_type = table.Column<int>(type: "int", nullable: true),
                    id_car_drive_type = table.Column<int>(type: "int", nullable: true),
                    id_car_transmission_type = table.Column<int>(type: "int", nullable: true),
                    engine_capacity = table.Column<int>(type: "int", nullable: true),
                    engine_power = table.Column<int>(type: "int", nullable: true),
                    fuel_consumption_combined = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true),
                    ground_clearance = table.Column<decimal>(type: "decimal(5,2)", precision: 5, scale: 2, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_modification);
                    table.ForeignKey(
                        name: "fk_car_drive_type",
                        column: x => x.id_car_drive_type,
                        principalTable: "car_drive_type",
                        principalColumn: "id_car_drive_type",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_engine_type",
                        column: x => x.id_car_engine_type,
                        principalTable: "car_engine_type",
                        principalColumn: "id_car_engine_type",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_model",
                        column: x => x.id_car_model,
                        principalTable: "car_model",
                        principalColumn: "id_car_model",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_serie",
                        column: x => x.id_car_serie,
                        principalTable: "car_serie",
                        principalColumn: "id_car_serie",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_transmission_type",
                        column: x => x.id_car_transmission_type,
                        principalTable: "car_transmission_type",
                        principalColumn: "id_car_transmission_type",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "fk_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Модификации автомобилей")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_characteristic_value",
                columns: table => new
                {
                    id_car_characteristic_value = table.Column<int>(type: "int", nullable: false),
                    value = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    unit = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: true, comment: "Еденица измерения", collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    id_car_characteristic = table.Column<int>(type: "int", nullable: false),
                    id_car_modification = table.Column<int>(type: "int", nullable: false),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_characteristic_value);
                    table.ForeignKey(
                        name: "fk_car_characteristic_value_id_car_characteristic",
                        column: x => x.id_car_characteristic,
                        principalTable: "car_characteristic",
                        principalColumn: "id_car_characteristic",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_characteristic_value_id_car_modification",
                        column: x => x.id_car_modification,
                        principalTable: "car_modification",
                        principalColumn: "id_car_modification",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_characteristic_value_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значения характеристик автомобиля")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_equipment",
                columns: table => new
                {
                    id_car_equipment = table.Column<int>(type: "int", nullable: false, comment: "id"),
                    name = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false, collation: "utf8mb3_general_ci")
                        .Annotation("MySql:CharSet", "utf8mb3"),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: true),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: true),
                    id_car_modification = table.Column<int>(type: "int", nullable: false),
                    price_min = table.Column<int>(type: "int", nullable: true, comment: "Цена от"),
                    id_car_type = table.Column<int>(type: "int", nullable: false),
                    year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_equipment);
                    table.ForeignKey(
                        name: "fk_car_equipment_id_car_modification",
                        column: x => x.id_car_modification,
                        principalTable: "car_modification",
                        principalColumn: "id_car_modification",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_equipment_id_car_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Комплектации")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateTable(
                name: "car_option_value",
                columns: table => new
                {
                    id_car_option_value = table.Column<int>(type: "int", nullable: false),
                    is_base = table.Column<bool>(type: "tinyint(1)", nullable: false, defaultValueSql: "'1'"),
                    id_car_option = table.Column<int>(type: "int", nullable: false),
                    id_car_equipment = table.Column<int>(type: "int", nullable: false),
                    date_create = table.Column<uint>(type: "int unsigned", nullable: false),
                    date_update = table.Column<uint>(type: "int unsigned", nullable: false),
                    id_car_type = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_car_option_value);
                    table.ForeignKey(
                        name: "fk_car_option_value_equipment",
                        column: x => x.id_car_equipment,
                        principalTable: "car_equipment",
                        principalColumn: "id_car_equipment",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_option_value_option",
                        column: x => x.id_car_option,
                        principalTable: "car_option",
                        principalColumn: "id_car_option",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_car_option_value_type",
                        column: x => x.id_car_type,
                        principalTable: "car_type",
                        principalColumn: "id_car_type",
                        onDelete: ReferentialAction.Cascade);
                },
                comment: "Значения опций")
                .Annotation("MySql:CharSet", "utf8mb3")
                .Annotation("Relational:Collation", "utf8mb3_general_ci");

            migrationBuilder.CreateIndex(
                name: "fk_car_characteristic_id_parent",
                table: "car_characteristic",
                column: "id_parent");

            migrationBuilder.CreateIndex(
                name: "id_car_type1",
                table: "car_characteristic",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "name",
                table: "car_characteristic",
                columns: new[] { "name", "id_parent", "id_car_type" });

            migrationBuilder.CreateIndex(
                name: "fk_car_characteristic_value_id_car_modification",
                table: "car_characteristic_value",
                column: "id_car_modification");

            migrationBuilder.CreateIndex(
                name: "id_car_characteristic",
                table: "car_characteristic_value",
                columns: new[] { "id_car_characteristic", "id_car_modification", "id_car_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "id_car_type2",
                table: "car_characteristic_value",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "id_characteristic",
                table: "car_characteristic_value",
                columns: new[] { "id_car_characteristic", "id_car_modification" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "date_delete",
                table: "car_equipment",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "fk_car_equipment_id_car_modification",
                table: "car_equipment",
                column: "id_car_modification");

            migrationBuilder.CreateIndex(
                name: "id_car_model",
                table: "car_generation",
                column: "id_car_model");

            migrationBuilder.CreateIndex(
                name: "id_car_type3",
                table: "car_generation",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "id_car_type",
                table: "car_mark",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "id_car_mark",
                table: "car_model",
                column: "id_car_mark");

            migrationBuilder.CreateIndex(
                name: "id_car_type4",
                table: "car_model",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "name1",
                table: "car_model",
                column: "name");

            migrationBuilder.CreateIndex(
                name: "id_car_drive_type",
                table: "car_modification",
                column: "id_car_drive_type");

            migrationBuilder.CreateIndex(
                name: "id_car_engine_type",
                table: "car_modification",
                column: "id_car_engine_type");

            migrationBuilder.CreateIndex(
                name: "id_car_model1",
                table: "car_modification",
                column: "id_car_model");

            migrationBuilder.CreateIndex(
                name: "id_car_serie",
                table: "car_modification",
                column: "id_car_serie");

            migrationBuilder.CreateIndex(
                name: "id_car_transmission_type",
                table: "car_modification",
                column: "id_car_transmission_type");

            migrationBuilder.CreateIndex(
                name: "id_car_type5",
                table: "car_modification",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "fk_car_option_parent",
                table: "car_option",
                column: "id_parent");

            migrationBuilder.CreateIndex(
                name: "id_car_type6",
                table: "car_option",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "date_delete1",
                table: "car_option_value",
                column: "id_car_type");

            migrationBuilder.CreateIndex(
                name: "fk_car_option_value_equipment",
                table: "car_option_value",
                column: "id_car_equipment");

            migrationBuilder.CreateIndex(
                name: "id_car_option",
                table: "car_option_value",
                columns: new[] { "id_car_option", "id_car_equipment", "id_car_type" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "fk_car_generation_serie",
                table: "car_serie",
                column: "id_car_generation");

            migrationBuilder.CreateIndex(
                name: "id_car_body_type",
                table: "car_serie",
                column: "id_car_body_type");

            migrationBuilder.CreateIndex(
                name: "id_car_model2",
                table: "car_serie",
                column: "id_car_model");

            migrationBuilder.CreateIndex(
                name: "id_car_type7",
                table: "car_serie",
                column: "id_car_type");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_characteristic_value");

            migrationBuilder.DropTable(
                name: "car_color");

            migrationBuilder.DropTable(
                name: "car_condition");

            migrationBuilder.DropTable(
                name: "car_exchange_option");

            migrationBuilder.DropTable(
                name: "car_interior_color");

            migrationBuilder.DropTable(
                name: "car_interior_material");

            migrationBuilder.DropTable(
                name: "car_option_value");

            migrationBuilder.DropTable(
                name: "car_characteristic");

            migrationBuilder.DropTable(
                name: "car_equipment");

            migrationBuilder.DropTable(
                name: "car_option");

            migrationBuilder.DropTable(
                name: "car_modification");

            migrationBuilder.DropTable(
                name: "car_drive_type");

            migrationBuilder.DropTable(
                name: "car_engine_type");

            migrationBuilder.DropTable(
                name: "car_serie");

            migrationBuilder.DropTable(
                name: "car_transmission_type");

            migrationBuilder.DropTable(
                name: "car_body_type");

            migrationBuilder.DropTable(
                name: "car_generation");

            migrationBuilder.DropTable(
                name: "car_model");

            migrationBuilder.DropTable(
                name: "car_mark");

            migrationBuilder.DropTable(
                name: "car_type");
        }
    }
}
