using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SmartCrop.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        ///
        /// Defining a constant for INTEGER type to avoid repetition, as per linter's suggestion
        private const string IntegerType = "INTEGER";
        /// Defining a constant for the autoincrement annotation to avoid repetition
        private const string AutoincrementAnnotation = "Sqlite:Autoincrement";
        
        // Defining table names as constants to avoid repetition
        private const string FieldsTableName = "Fields";
        private const string FarmersTableName = "Farmers";
        private const string CropsTableName = "Crops";
        private const string SoilDataTableName = "SoilData";
        private const string HealthStatusesTableName = "HealthStatuses";
        private const string RecommendationsTableName = "Recommendations";
        private const string WeatherDataTableName = "WeatherData";
        
        // Defining constants for some of the column names to avoid repetition
        private const string FarmerIdColumn = "FarmerId";
        private const string FieldIdColumn = "FieldId";
        private const string CropIdColumn = "CropId";
        
        
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: FarmersTableName,
                columns: table => new
                {
                    FarmerId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farmers", x => x.FarmerId);
                });

            migrationBuilder.CreateTable(
                name: FieldsTableName,
                columns: table => new
                {
                    FieldId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    Location = table.Column<string>(type: "TEXT", nullable: false),
                    SoilType = table.Column<string>(type: "TEXT", nullable: false),
                    FarmerId = table.Column<int>(type: IntegerType, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.FieldId);
                    table.ForeignKey(
                        name: "FK_Fields_Farmers_FarmerId",
                        column: x => x.FarmerId,
                        principalTable: FarmersTableName,
                        principalColumn: FarmerIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: CropsTableName,
                columns: table => new
                {
                    CropId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Variety = table.Column<string>(type: "TEXT", nullable: false),
                    FieldId = table.Column<int>(type: IntegerType, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crops", x => x.CropId);
                    table.ForeignKey(
                        name: "FK_Crops_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: FieldsTableName,
                        principalColumn: FieldIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: SoilDataTableName,
                columns: table => new
                {
                    SoilDataId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    FieldId = table.Column<int>(type: IntegerType, nullable: false),
                    SoilType = table.Column<string>(type: "TEXT", nullable: false),
                    SoilMoisture = table.Column<string>(type: "TEXT", nullable: false),
                    SoilpH = table.Column<string>(type: "TEXT", nullable: false),
                    SoilOrganicMatter = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SoilData", x => x.SoilDataId);
                    table.ForeignKey(
                        name: "FK_SoilData_Fields_FieldId",
                        column: x => x.FieldId,
                        principalTable: FieldsTableName,
                        principalColumn: FieldIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: HealthStatusesTableName,
                columns: table => new
                {
                    HealthStatusId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    CropId = table.Column<int>(type: IntegerType, nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false),
                    DateChecked = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Notes = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HealthStatuses", x => x.HealthStatusId);
                    table.ForeignKey(
                        name: "FK_HealthStatuses_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: CropsTableName,
                        principalColumn: CropIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: RecommendationsTableName,
                columns: table => new
                {
                    RecommendationId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    CropId = table.Column<int>(type: IntegerType, nullable: false),
                    RecommendationText = table.Column<string>(type: "TEXT", nullable: false),
                    DateIssued = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Priority = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Recommendations", x => x.RecommendationId);
                    table.ForeignKey(
                        name: "FK_Recommendations_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: CropsTableName,
                        principalColumn: CropIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: WeatherDataTableName,
                columns: table => new
                {
                    WeatherDataId = table.Column<int>(type: IntegerType, nullable: false)
                        .Annotation(AutoincrementAnnotation, true),
                    CropId = table.Column<int>(type: IntegerType, nullable: false),
                    Date = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Temperature = table.Column<double>(type: "REAL", nullable: false),
                    Humidity = table.Column<double>(type: "REAL", nullable: false),
                    Rain = table.Column<double>(type: "REAL", nullable: false),
                    UVi = table.Column<double>(type: "REAL", nullable: false),
                    Clouds = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeatherData", x => x.WeatherDataId);
                    table.ForeignKey(
                        name: "FK_WeatherData_Crops_CropId",
                        column: x => x.CropId,
                        principalTable: CropsTableName,
                        principalColumn: CropIdColumn,
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Crops_FieldId",
                table: CropsTableName,
                column: FieldIdColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Fields_FarmerId",
                table: FieldsTableName,
                column: FarmerIdColumn);

            migrationBuilder.CreateIndex(
                name: "IX_HealthStatuses_CropId",
                table: HealthStatusesTableName,
                column: CropIdColumn);

            migrationBuilder.CreateIndex(
                name: "IX_Recommendations_CropId",
                table: RecommendationsTableName,
                column: CropIdColumn);

            migrationBuilder.CreateIndex(
                name: "IX_SoilData_FieldId",
                table: SoilDataTableName,
                column: FieldIdColumn);

            migrationBuilder.CreateIndex(
                name: "IX_WeatherData_CropId",
                table: WeatherDataTableName,
                column: CropIdColumn);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: HealthStatusesTableName);

            migrationBuilder.DropTable(
                name: RecommendationsTableName);

            migrationBuilder.DropTable(
                name: SoilDataTableName);

            migrationBuilder.DropTable(
                name: WeatherDataTableName);

            migrationBuilder.DropTable(
                name: CropsTableName);

            migrationBuilder.DropTable(
                name: FieldsTableName);

            migrationBuilder.DropTable(
                name: FarmersTableName);
        }
    }
}
