using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateQueueElementSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SongChangedAt",
                table: "Lobbies");

            migrationBuilder.AddColumn<DateTime>(
                name: "PlayedAt",
                table: "QueueElements",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PlayedAt",
                table: "QueueElements");

            migrationBuilder.AddColumn<DateTime>(
                name: "SongChangedAt",
                table: "Lobbies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
