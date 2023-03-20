using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSong : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Songs_Songs_SongId",
                table: "Songs");

            migrationBuilder.DropIndex(
                name: "IX_Songs_SongId",
                table: "Songs");

            migrationBuilder.DropColumn(
                name: "SongId",
                table: "Songs");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SongId",
                table: "Songs",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Songs_SongId",
                table: "Songs",
                column: "SongId");

            migrationBuilder.AddForeignKey(
                name: "FK_Songs_Songs_SongId",
                table: "Songs",
                column: "SongId",
                principalTable: "Songs",
                principalColumn: "Id");
        }
    }
}
