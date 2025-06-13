using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace geo.Migrations
{
    /// <inheritdoc />
    public partial class addVolumecode : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "VisualizationData",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SurveyId = table.Column<Guid>(type: "uuid", nullable: false),
                    AnotationId = table.Column<Guid>(type: "uuid", nullable: false),
                    Volume_Cut = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Fill = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Net = table.Column<double>(type: "double precision", nullable: false),
                    Volume_Total = table.Column<double>(type: "double precision", nullable: false),
                    Status = table.Column<string>(type: "text", nullable: false),
                    Timestamp = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VisualizationData", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VisualizationData");
        }
    }
}
