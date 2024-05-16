using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatebooks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.AddColumn<int>(
                name: "AuthorId",
                table: "Books",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "Name" },
                values: new object[,]
                {
                    { new Guid("3d81f6d4-00a2-4174-9c01-73ec6ba96db8"), "Karuhatan", "Alvin" },
                    { new Guid("6f589372-fcc5-476c-9754-e3c4cd430bff"), "Valenzuela", "Amara" },
                    { new Guid("972c1ddc-e5b4-4ce4-917b-110eb87447b2"), "Manila", "Andrei" },
                    { new Guid("9ac418f8-cb68-4f7e-80e7-00263711808b"), "Davao", "Kresleen" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Books_AuthorId",
                table: "Books",
                column: "AuthorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books",
                column: "AuthorId",
                principalTable: "Authors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Books_Authors_AuthorId",
                table: "Books");

            migrationBuilder.DropIndex(
                name: "IX_Books_AuthorId",
                table: "Books");

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("3d81f6d4-00a2-4174-9c01-73ec6ba96db8"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("6f589372-fcc5-476c-9754-e3c4cd430bff"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("972c1ddc-e5b4-4ce4-917b-110eb87447b2"));

            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: new Guid("9ac418f8-cb68-4f7e-80e7-00263711808b"));

            migrationBuilder.DropColumn(
                name: "AuthorId",
                table: "Books");

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
    }
}
