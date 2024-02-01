using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyWeb.Migrations
{
    /// <inheritdoc />
    public partial class SeedAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "ImageUrl", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("09afc315-84da-4afb-88d1-61fd12e529fb"), null, "admin.png", "Admin", "ADMIN" },
                    { new Guid("3d715209-81e9-4307-a304-e626b78d90d2"), null, "user.png", "User", "USER" },
                    { new Guid("bb774034-38c1-45a5-bee9-e30377ffe312"), null, "musicmanager.png", "SongManager", "SONGMANAGER" },
                    { new Guid("d758bb58-1585-463d-a892-75b9a05c7145"), null, "moderator.png", "AccountManager", "ACCOUNTMANAGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LastAccessed", "LockoutEnabled", "LockoutEnd", "NickName", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("f2c3b2fd-1e04-40e7-9241-183d86c47e70"), 0, "70052648-cd49-44de-8461-0b2b993a84c9", "admin@admin.com", false, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, null, "Admin", "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAIAAYagAAAAEK2EA68QZWx32k7LLdXxtCDxgX16tzWPdP1rnzcmWZXPpG3HFT5h8BLwVpVSrJCmRA==", null, false, "bc998d34-b323-4371-ac6b-bb8fd37fb9e1", false, "admin@admin.com" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { new Guid("09afc315-84da-4afb-88d1-61fd12e529fb"), new Guid("f2c3b2fd-1e04-40e7-9241-183d86c47e70") });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("3d715209-81e9-4307-a304-e626b78d90d2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("bb774034-38c1-45a5-bee9-e30377ffe312"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("d758bb58-1585-463d-a892-75b9a05c7145"));

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { new Guid("09afc315-84da-4afb-88d1-61fd12e529fb"), new Guid("f2c3b2fd-1e04-40e7-9241-183d86c47e70") });

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("09afc315-84da-4afb-88d1-61fd12e529fb"));

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("f2c3b2fd-1e04-40e7-9241-183d86c47e70"));

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
    }
}
