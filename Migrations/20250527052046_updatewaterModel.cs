using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class updatewaterModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Sensor_type",
                table: "WaterQualityData",
                type: "text",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Sensor_type",
                table: "WaterQualityData");
        }
    }
}
