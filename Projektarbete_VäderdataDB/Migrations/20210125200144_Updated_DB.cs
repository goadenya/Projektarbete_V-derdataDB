using Microsoft.EntityFrameworkCore.Migrations;

namespace Projektarbete_VäderdataDB.Migrations
{
    public partial class Updated_DB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Temperatur",
                table: "Temperatures",
                newName: "Temperature");

            migrationBuilder.RenameColumn(
                name: "Plats",
                table: "Temperatures",
                newName: "Place");

            migrationBuilder.RenameColumn(
                name: "Luftfuktighet",
                table: "Temperatures",
                newName: "Humidity");

            migrationBuilder.RenameColumn(
                name: "Datum",
                table: "Temperatures",
                newName: "Date");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Temperature",
                table: "Temperatures",
                newName: "Temperatur");

            migrationBuilder.RenameColumn(
                name: "Place",
                table: "Temperatures",
                newName: "Plats");

            migrationBuilder.RenameColumn(
                name: "Humidity",
                table: "Temperatures",
                newName: "Luftfuktighet");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Temperatures",
                newName: "Datum");
        }
    }
}
