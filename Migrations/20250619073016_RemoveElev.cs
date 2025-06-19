using System;
using System.Text.Json;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class RemoveElev : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AirQualityData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    AQI = table.Column<int>(type: "integer", nullable: false),
                    Category = table.Column<string>(type: "text", nullable: false),
                    PM2_5 = table.Column<double>(type: "double precision", nullable: false),
                    PM10 = table.Column<double>(type: "double precision", nullable: false),
                    NO2 = table.Column<double>(type: "double precision", nullable: false),
                    CO = table.Column<double>(type: "double precision", nullable: false),
                    O3 = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AirQualityData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "ElectricMeterData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    HouseholdId = table.Column<string>(type: "text", nullable: false),
                    HouseArea = table.Column<string>(type: "text", nullable: false),
                    ConsumptionKWh = table.Column<double>(type: "double precision", nullable: false),
                    MeterStatus = table.Column<string>(type: "text", nullable: false),
                    BillingCycle = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricMeterData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "EnvironmentData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    TemperatureCelsius = table.Column<double>(type: "double precision", nullable: false),
                    HumidityPercent = table.Column<double>(type: "double precision", nullable: false),
                    UvIndex = table.Column<double>(type: "double precision", nullable: false),
                    NoiseLevelDb = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnvironmentData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "ResidentCountData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    ResidentialBlock = table.Column<string>(type: "text", nullable: false),
                    NumberOfResidents = table.Column<int>(type: "integer", nullable: false),
                    NumberOfHouseholds = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResidentCountData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "TrafficData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    VehicleCount = table.Column<int>(type: "integer", nullable: false),
                    AverageSpeedKmph = table.Column<double>(type: "double precision", nullable: false),
                    TrafficCongestionLevel = table.Column<string>(type: "text", nullable: false),
                    SignalViolations = table.Column<int>(type: "integer", nullable: false),
                    AccidentsReported = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "VisualizationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnotationId = table.Column<Guid>(type: "uuid", nullable: false),
                    ConstructionStageMasterId = table.Column<Guid>(type: "uuid", nullable: false),
                    Volume_Cut = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Fill = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Net = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Total = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Stage = table.Column<string>(type: "text", nullable: false),
                    Chainage_to = table.Column<decimal>(type: "numeric", nullable: false),
                    Chainage_from = table.Column<decimal>(type: "numeric", nullable: false),
                    Geometry = table.Column<JsonDocument>(type: "jsonb", nullable: true),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisualizationData", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterQualityData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    Sensor_type = table.Column<string>(type: "text", nullable: false),
                    Location_Lat = table.Column<double>(type: "double precision", nullable: false),
                    Location_Lon = table.Column<double>(type: "double precision", nullable: false),
                    PhLevel = table.Column<double>(type: "double precision", nullable: false),
                    TurbidityNTU = table.Column<double>(type: "double precision", nullable: false),
                    DissolvedOxygenMgPerL = table.Column<double>(type: "double precision", nullable: false),
                    ContaminantsPpm_Lead = table.Column<double>(type: "double precision", nullable: false),
                    ContaminantsPpm_Arsenic = table.Column<double>(type: "double precision", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
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
                name: "ElectricMeterData");

            migrationBuilder.DropTable(
                name: "EnvironmentData");

            migrationBuilder.DropTable(
                name: "ResidentCountData");

            migrationBuilder.DropTable(
                name: "TrafficData");

            migrationBuilder.DropTable(
                name: "VisualizationData");

            migrationBuilder.DropTable(
                name: "WaterQualityData");
        }
    }
}
