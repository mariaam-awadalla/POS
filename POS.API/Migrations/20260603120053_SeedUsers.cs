using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace POS.API.Migrations
{
    /// <inheritdoc />
    public partial class SeedUsers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Age", "CreatedAt", "Email", "Gender", "Name" },
                values: new object[,]
                {
                    { 1, 22, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "menna@gmail.com", "Female", "Menna Ahmed" },
                    { 2, 25, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "ahmed@gmail.com", "Male", "Ahmed Ali" },
                    { 3, 24, new DateTime(2026, 6, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "sara@gmail.com", "Female", "Sara Mohamed" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
