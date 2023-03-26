using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingSongs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Songs",
                columns: new[] { "Id", "Artist", "Duration", "GenreId", "Title", "URL" },
                values: new object[,]
                {
                    { 1, "Alex-Productions", 154000.0, 17, "Freedom", "track1.mp3" },
                    { 2, "Roa", 156000.0, 17, "Sunset Walk", "track2.mp3" },
                    { 3, "LiQWYD", 135000.0, 17, "The Light", "track3.mp3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Songs",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
