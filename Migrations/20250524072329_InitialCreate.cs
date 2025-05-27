using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirQualityData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    AQI = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    PM2_5 = table.Column<double>(type: "double precision", nullable: false),
                    PM10 = table.Column<double>(type: "double precision", nullable: false),
                    NO2 = table.Column<double>(type: "double precision", nullable: false),
                    CO = table.Column<double>(type: "double precision", nullable: false),
                    O3 = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualityData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    TemperatureCelsius = table.Column<double>(type: "double precision", nullable: false),
                    HumidityPercent = table.Column<double>(type: "double precision", nullable: false),
                    UvIndex = table.Column<double>(type: "double precision", nullable: false),
                    NoiseLevelDb = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "WaterQualityData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    PhLevel = table.Column<double>(type: "double precision", nullable: false),
                    TurbidityNTU = table.Column<double>(type: "double precision", nullable: false),
                    DissolvedOxygenMgPerL = table.Column<double>(type: "double precision", nullable: false),
                    ContaminantsPpm = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterQualityData", x => x.SensorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AirQualityData");

            migrationBuilder.DropTable(
                name: "EnvironmentData");

            migrationBuilder.DropTable(
                name: "WaterQualityData");
        }
    }
}
