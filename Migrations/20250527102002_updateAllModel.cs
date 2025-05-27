using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class updateAllModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sensor_type",
                table: "TrafficData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sensor_type",
                table: "ResidentCountData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sensor_type",
                table: "ElectricMeterData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Sensor_type",
                table: "AirQualityData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sensor_type",
                table: "TrafficData");

            migrationBuilder.DropColumn(
                name: "Sensor_type",
                table: "ResidentCountData");

            migrationBuilder.DropColumn(
                name: "Sensor_type",
                table: "ElectricMeterData");

            migrationBuilder.DropColumn(
                name: "Sensor_type",
                table: "AirQualityData");
        }
    }
}
