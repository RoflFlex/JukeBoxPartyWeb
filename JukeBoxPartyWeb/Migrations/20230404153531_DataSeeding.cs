using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyWeb.Migrations
{
    /// <inheritdoc />
    public partial class DataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ImageUrl", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("356ea5c6-cd9b-479b-840d-0342f13edc23"), null, "moderator.png", "AccountManager", null },
                    { new Guid("6c553387-2fae-46b5-a498-54af0aca5b67"), null, "musicmanager.png", "SongManager", null },
                    { new Guid("a2164aec-30ac-491e-8b51-4fd535198479"), null, "user.png", "User", null },
                    { new Guid("e79ede60-41ca-49fa-a2db-e9d22933c563"), null, "admin.png", "Admin", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("356ea5c6-cd9b-479b-840d-0342f13edc23"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("6c553387-2fae-46b5-a498-54af0aca5b67"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("a2164aec-30ac-491e-8b51-4fd535198479"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e79ede60-41ca-49fa-a2db-e9d22933c563"));
        }
    }
}
