using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace JukeBoxPartyAPI.Migrations
{
    /// <inheritdoc />
    public partial class QueueElementAddStudentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("074950e4-7108-45bb-8144-cd6c60e7bc26"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("7de18949-4e72-4967-ba3a-8f6d885f2ff7"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("c6614c17-9455-4124-922c-ddcac7e0d8b4"));

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Songs",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Artist",
                table: "Songs",
                type: "nvarchar(55)",
                maxLength: 55,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "QueueElements",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.InsertData(
                table: "Lobbies",
                columns: new[] { "Id", "CreatedAt", "IsActive" },
                values: new object[,]
                {
                    { new Guid("4996f687-7323-458d-ae5b-ba3ea567928b"), new DateTime(2023, 4, 18, 21, 40, 1, 505, DateTimeKind.Local).AddTicks(2170), true },
                    { new Guid("b5037ff4-3785-41eb-aeac-8dc9463ec93b"), new DateTime(2023, 4, 18, 21, 40, 1, 505, DateTimeKind.Local).AddTicks(2175), true },
                    { new Guid("e7bb933f-0b10-4e3b-920f-3b5354ea1e0a"), new DateTime(2023, 4, 18, 21, 40, 1, 505, DateTimeKind.Local).AddTicks(2086), true }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("4996f687-7323-458d-ae5b-ba3ea567928b"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("b5037ff4-3785-41eb-aeac-8dc9463ec93b"));

            migrationBuilder.DeleteData(
                table: "Lobbies",
                keyColumn: "Id",
                keyValue: new Guid("e7bb933f-0b10-4e3b-920f-3b5354ea1e0a"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "QueueElements");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(200)",
                oldMaxLength: 200);

            migrationBuilder.AlterColumn<string>(
                name: "Artist",
                table: "Songs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(55)",
                oldMaxLength: 55);

            migrationBuilder.InsertData(
                table: "Lobbies",
                columns: new[] { "Id", "CreatedAt", "IsActive" },
                values: new object[,]
                {
                    { new Guid("074950e4-7108-45bb-8144-cd6c60e7bc26"), new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(148), true },
                    { new Guid("7de18949-4e72-4967-ba3a-8f6d885f2ff7"), new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(208), true },
                    { new Guid("c6614c17-9455-4124-922c-ddcac7e0d8b4"), new DateTime(2023, 3, 26, 20, 26, 44, 856, DateTimeKind.Local).AddTicks(211), true }
                });
        }
    }
}
