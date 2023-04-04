using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyWeb.Migrations
{
    /// <inheritdoc />
    public partial class DataSeedRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ImageUrl", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("12c2bda6-b76c-4b9b-959b-4a266071c345"), null, "user.png", "User", "USER" },
                    { new Guid("7f043c9a-1fe9-4865-8d88-283a6400e5af"), null, "musicmanager.png", "SongManager", "SONGMANAGER" },
                    { new Guid("b3a0993e-d159-4162-b5bc-2de3df5459b0"), null, "admin.png", "Admin", "ADMIN" },
                    { new Guid("bc0ac5b3-4270-4435-bfcc-c168b0edca3d"), null, "moderator.png", "AccountManager", "ACCOUNTMANAGER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("12c2bda6-b76c-4b9b-959b-4a266071c345"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("7f043c9a-1fe9-4865-8d88-283a6400e5af"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("b3a0993e-d159-4162-b5bc-2de3df5459b0"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bc0ac5b3-4270-4435-bfcc-c168b0edca3d"));
        }
    }
}
