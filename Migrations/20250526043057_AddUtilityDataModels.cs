using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class AddUtilityDataModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ElectricMeterData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    HouseholdId = table.Column<string>(type: "text", nullable: false),
                    HouseArea = table.Column<string>(type: "text", nullable: false),
                    ConsumptionKWh = table.Column<double>(type: "double precision", nullable: false),
                    MeterStatus = table.Column<string>(type: "text", nullable: false),
                    BillingCycle = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricMeterData", x => x.SensorId);
                });

            migrationBuilder.CreateTable(
                name: "ResidentCountData",
                columns: table => new
                {
                    SensorId = table.Column<string>(type: "text", nullable: false),
                    ResidentialBlock = table.Column<string>(type: "text", nullable: false),
                    NumberOfResidents = table.Column<int>(type: "integer", nullable: false),
                    NumberOfHouseholds = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
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
                    VehicleCount = table.Column<int>(type: "integer", nullable: false),
                    AverageSpeedKmph = table.Column<double>(type: "double precision", nullable: false),
                    TrafficCongestionLevel = table.Column<string>(type: "text", nullable: false),
                    SignalViolations = table.Column<int>(type: "integer", nullable: false),
                    AccidentsReported = table.Column<int>(type: "integer", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Location = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrafficData", x => x.SensorId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricMeterData");

            migrationBuilder.DropTable(
                name: "ResidentCountData");

            migrationBuilder.DropTable(
                name: "TrafficData");
        }
    }
}
