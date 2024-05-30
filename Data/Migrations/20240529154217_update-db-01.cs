using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb01 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_Employees_PrincipalId",
                table: "Dependents");

            migrationBuilder.RenameColumn(
                name: "PrincipalId",
                table: "Dependents",
                newName: "EmployeeId");

            migrationBuilder.RenameIndex(
                name: "IX_Dependents_PrincipalId",
                table: "Dependents",
                newName: "IX_Dependents_EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_Employees_EmployeeId",
                table: "Dependents",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_Employees_EmployeeId",
                table: "Dependents");

            migrationBuilder.RenameColumn(
                name: "EmployeeId",
                table: "Dependents",
                newName: "PrincipalId");

            migrationBuilder.RenameIndex(
                name: "IX_Dependents_EmployeeId",
                table: "Dependents",
                newName: "IX_Dependents_PrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_Employees_PrincipalId",
                table: "Dependents",
                column: "PrincipalId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
