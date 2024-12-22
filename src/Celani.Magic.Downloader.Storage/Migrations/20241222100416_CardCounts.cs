using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celani.Magic.Downloader.Storage.Migrations
{
    /// <inheritdoc />
    public partial class CardCounts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CardCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CardCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CardCounts_OracleCards_CardId",
                        column: x => x.CardId,
                        principalTable: "OracleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CardCounts_CardId",
                table: "CardCounts",
                column: "CardId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CardCounts");
        }
    }
}
