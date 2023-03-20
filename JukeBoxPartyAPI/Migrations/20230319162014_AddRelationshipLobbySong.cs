using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class AddRelationshipLobbySong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Lobbies_LobbyId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_LobbyId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "LobbyId",
                table: "Songs");

            migrationBuilder.CreateTable(
                name: "QueueElements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LobbyId = table.Column<int>(type: "int", nullable: false),
                    SongId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QueueElements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QueueElements_Lobbies_LobbyId",
                        column: x => x.LobbyId,
                        principalTable: "Lobbies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_QueueElements_Songs_SongId",
                        column: x => x.SongId,
                        principalTable: "Songs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QueueElements_LobbyId",
                table: "QueueElements",
                column: "LobbyId");

            migrationBuilder.CreateIndex(
                name: "IX_QueueElements_SongId",
                table: "QueueElements",
                column: "SongId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "QueueElements");

            migrationBuilder.AddColumn<int>(
                name: "LobbyId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_LobbyId",
                table: "Songs",
                column: "LobbyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Lobbies_LobbyId",
                table: "Songs",
                column: "LobbyId",
                principalTable: "Lobbies",
                principalColumn: "Id");
        }
    }
}
