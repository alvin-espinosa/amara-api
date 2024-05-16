using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "Authors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Authors", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Books",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Books", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("555f6a56-4142-4852-a77e-7a78ef831663"), "Manila", "Andrei" },
                    { new Guid("8f05004a-6b03-47b4-a0e4-7494055ec90d"), "Valenzuela", "Amara" },
                    { new Guid("b68c37c5-8724-40aa-a161-323c2c46cc1f"), "Karuhatan", "Alvin" },
                    { new Guid("c386717b-3eb9-4c48-90ef-dcc76806e2bf"), "Davao", "Kresleen" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Authors");

            migrationBuilder.DropTable(
                name: "Books");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("555f6a56-4142-4852-a77e-7a78ef831663"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("8f05004a-6b03-47b4-a0e4-7494055ec90d"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("b68c37c5-8724-40aa-a161-323c2c46cc1f"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("c386717b-3eb9-4c48-90ef-dcc76806e2bf"));

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
    }
}
