using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Celani.Magic.Downloader.Storage.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Metadata",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    DownloadUri = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Metadata", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OracleCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    OracleId = table.Column<string>(type: "TEXT", nullable: false),
                    RepresentativeScryfallId = table.Column<string>(type: "TEXT", nullable: false),
                    ColorIdentity = table.Column<int>(type: "INTEGER", nullable: false),
                    CardTypes = table.Column<int>(type: "INTEGER", nullable: false),
                    Layout = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OracleCards", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Commanders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommanderId = table.Column<int>(type: "INTEGER", nullable: false),
                    PartnerId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Commanders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Commanders_OracleCards_CommanderId",
                        column: x => x.CommanderId,
                        principalTable: "OracleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Commanders_OracleCards_PartnerId",
                        column: x => x.PartnerId,
                        principalTable: "OracleCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "ScryfallCards",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ScryfallId = table.Column<string>(type: "TEXT", nullable: false),
                    OracleCardId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ScryfallCards", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ScryfallCards_OracleCards_OracleCardId",
                        column: x => x.OracleCardId,
                        principalTable: "OracleCards",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CommanderCardCounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CommanderId = table.Column<int>(type: "INTEGER", nullable: false),
                    CardId = table.Column<int>(type: "INTEGER", nullable: false),
                    Count = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CommanderCardCounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CommanderCardCounts_Commanders_CommanderId",
                        column: x => x.CommanderId,
                        principalTable: "Commanders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CommanderCardCounts_OracleCards_CardId",
                        column: x => x.CardId,
                        principalTable: "OracleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Decks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Source = table.Column<string>(type: "TEXT", nullable: true),
                    SourceId = table.Column<string>(type: "TEXT", nullable: true),
                    MagicCommanderId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Decks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Decks_Commanders_MagicCommanderId",
                        column: x => x.MagicCommanderId,
                        principalTable: "Commanders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OracleCardStoredDeck",
                columns: table => new
                {
                    CardsId = table.Column<int>(type: "INTEGER", nullable: false),
                    StoredDecksId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OracleCardStoredDeck", x => new { x.CardsId, x.StoredDecksId });
                    table.ForeignKey(
                        name: "FK_OracleCardStoredDeck_Decks_StoredDecksId",
                        column: x => x.StoredDecksId,
                        principalTable: "Decks",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OracleCardStoredDeck_OracleCards_CardsId",
                        column: x => x.CardsId,
                        principalTable: "OracleCards",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CommanderCardCounts_CardId",
                table: "CommanderCardCounts",
                column: "CardId");

            migrationBuilder.CreateIndex(
                name: "IX_CommanderCardCounts_CommanderId_CardId",
                table: "CommanderCardCounts",
                columns: new[] { "CommanderId", "CardId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commanders_CommanderId_PartnerId",
                table: "Commanders",
                columns: new[] { "CommanderId", "PartnerId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Commanders_PartnerId",
                table: "Commanders",
                column: "PartnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_MagicCommanderId",
                table: "Decks",
                column: "MagicCommanderId");

            migrationBuilder.CreateIndex(
                name: "IX_Decks_Source_SourceId",
                table: "Decks",
                columns: new[] { "Source", "SourceId" });

            migrationBuilder.CreateIndex(
                name: "IX_OracleCards_Name",
                table: "OracleCards",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_OracleCards_OracleId",
                table: "OracleCards",
                column: "OracleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_OracleCardStoredDeck_StoredDecksId",
                table: "OracleCardStoredDeck",
                column: "StoredDecksId");

            migrationBuilder.CreateIndex(
                name: "IX_ScryfallCards_OracleCardId",
                table: "ScryfallCards",
                column: "OracleCardId");

            migrationBuilder.CreateIndex(
                name: "IX_ScryfallCards_ScryfallId",
                table: "ScryfallCards",
                column: "ScryfallId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CommanderCardCounts");

            migrationBuilder.DropTable(
                name: "Metadata");

            migrationBuilder.DropTable(
                name: "OracleCardStoredDeck");

            migrationBuilder.DropTable(
                name: "ScryfallCards");

            migrationBuilder.DropTable(
                name: "Decks");

            migrationBuilder.DropTable(
                name: "Commanders");

            migrationBuilder.DropTable(
                name: "OracleCards");
        }
    }
}
