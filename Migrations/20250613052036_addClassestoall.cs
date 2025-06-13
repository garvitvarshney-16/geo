using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class addClassestoall : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "TrafficData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ResidentCountData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "EnvironmentData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "ElectricMeterData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "AirQualityData");

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "TrafficData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "TrafficData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "ResidentCountData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "ResidentCountData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "EnvironmentData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "EnvironmentData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "ElectricMeterData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "ElectricMeterData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "AirQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "AirQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "TrafficData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "TrafficData");

            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "ResidentCountData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "ResidentCountData");

            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "EnvironmentData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "EnvironmentData");

            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "ElectricMeterData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "ElectricMeterData");

            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "AirQualityData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "AirQualityData");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "TrafficData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ResidentCountData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "EnvironmentData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "ElectricMeterData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "AirQualityData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
