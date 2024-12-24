using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celani.Magic.Downloader.Storage.Migrations
{
    /// <inheritdoc />
    public partial class AddOracleData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Layout",
                table: "OracleCards");

            migrationBuilder.AddColumn<string>(
                name: "ManaCost",
                table: "OracleCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "OracleText",
                table: "OracleCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "TypeLine",
                table: "OracleCards",
                type: "TEXT",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ManaCost",
                table: "OracleCards");

            migrationBuilder.DropColumn(
                name: "OracleText",
                table: "OracleCards");

            migrationBuilder.DropColumn(
                name: "TypeLine",
                table: "OracleCards");

            migrationBuilder.AddColumn<int>(
                name: "Layout",
                table: "OracleCards",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }
    }
}
