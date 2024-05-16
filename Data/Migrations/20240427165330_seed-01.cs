using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class seed01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("39ea581e-4a46-40e1-855d-1378820bc71a"), "Karuhatan", "Alvin" },
                    { new Guid("c5affdd6-e680-4fd6-8353-a8f5779611d2"), "Manila", "Andrei" },
                    { new Guid("db610892-dff2-4184-b4ab-75596a780ba7"), "Davao", "Kresleen" },
                    { new Guid("fe244ea1-f01e-4cce-a281-8460b88be94b"), "Valenzuela", "Amara" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("39ea581e-4a46-40e1-855d-1378820bc71a"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("c5affdd6-e680-4fd6-8353-a8f5779611d2"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("db610892-dff2-4184-b4ab-75596a780ba7"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("fe244ea1-f01e-4cce-a281-8460b88be94b"));
        }
    }
}
