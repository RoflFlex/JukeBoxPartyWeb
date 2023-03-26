using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedingLobbies : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Lobbies",
                columns: new[] { "Id", "CreatedAt", "IsActive" },
                values: new object[,]
                {
                    { new Guid("1262efd4-eec6-4523-a442-5837b6bb6460"), new DateTime(2023, 3, 26, 18, 35, 20, 354, DateTimeKind.Local).AddTicks(5380), true },
                    { new Guid("1d3739d5-f607-4376-afe1-57c6f14f3636"), new DateTime(2023, 3, 26, 18, 35, 20, 354, DateTimeKind.Local).AddTicks(5439), true },
                    { new Guid("3773c5d4-9094-4fcb-aff1-d9650ab1aab9"), new DateTime(2023, 3, 26, 18, 35, 20, 354, DateTimeKind.Local).AddTicks(5436), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("1262efd4-eec6-4523-a442-5837b6bb6460"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("1d3739d5-f607-4376-afe1-57c6f14f3636"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("3773c5d4-9094-4fcb-aff1-d9650ab1aab9"));
        }
    }
}
