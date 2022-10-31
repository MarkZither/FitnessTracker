using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FitnessTracker.Maui.Data.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Route",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "TrackerLocation",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Routeid = table.Column<int>(type: "INTEGER", nullable: true),
                    Timestamp = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Latitude = table.Column<double>(type: "REAL", nullable: false),
                    Longitude = table.Column<double>(type: "REAL", nullable: false),
                    Altitude = table.Column<double>(type: "REAL", nullable: true),
                    Accuracy = table.Column<double>(type: "REAL", nullable: true),
                    VerticalAccuracy = table.Column<double>(type: "REAL", nullable: true),
                    ReducedAccuracy = table.Column<bool>(type: "INTEGER", nullable: false),
                    Speed = table.Column<double>(type: "REAL", nullable: true),
                    Course = table.Column<double>(type: "REAL", nullable: true),
                    IsFromMockProvider = table.Column<bool>(type: "INTEGER", nullable: false),
                    AltitudeReferenceSystem = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrackerLocation", x => x.id);
                    table.ForeignKey(
                        name: "FK_TrackerLocation_Route_Routeid",
                        column: x => x.Routeid,
                        principalTable: "Route",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_TrackerLocation_Routeid",
                table: "TrackerLocation",
                column: "Routeid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TrackerLocation");

            migrationBuilder.DropTable(
                name: "Route");
        }
    }
}
