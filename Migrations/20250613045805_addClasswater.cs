using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class addClasswater : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaminantsPpm",
                table: "WaterQualityData");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "WaterQualityData");

            migrationBuilder.AddColumn<double>(
                name: "ContaminantsPpm_Arsenic",
                table: "WaterQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "ContaminantsPpm_Lead",
                table: "WaterQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lat",
                table: "WaterQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "Location_Lon",
                table: "WaterQualityData",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ContaminantsPpm_Arsenic",
                table: "WaterQualityData");

            migrationBuilder.DropColumn(
                name: "ContaminantsPpm_Lead",
                table: "WaterQualityData");

            migrationBuilder.DropColumn(
                name: "Location_Lat",
                table: "WaterQualityData");

            migrationBuilder.DropColumn(
                name: "Location_Lon",
                table: "WaterQualityData");

            migrationBuilder.AddColumn<string>(
                name: "ContaminantsPpm",
                table: "WaterQualityData",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "WaterQualityData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }
    }
}
