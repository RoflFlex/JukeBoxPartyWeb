using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class SongDurationAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Duration",
                table: "Songs",
                type: "float",
                nullable: false,
                defaultValue: 0.0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Duration",
                table: "Songs");
        }
    }
}
