using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class updatedb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependent_Employees_EmployeeId",
                table: "Dependent");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Spouse_SpouseId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spouse",
                table: "Spouse");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependent",
                table: "Dependent");

            migrationBuilder.DropIndex(
                name: "IX_Dependent_EmployeeId",
                table: "Dependent");

            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Dependent");

            migrationBuilder.RenameTable(
                name: "Spouse",
                newName: "Spouses");

            migrationBuilder.RenameTable(
                name: "Dependent",
                newName: "Dependents");

            migrationBuilder.AddColumn<Guid>(
                name: "PrincipalId",
                table: "Dependents",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spouses",
                table: "Spouses",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dependents_PrincipalId",
                table: "Dependents",
                column: "PrincipalId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependents_Employees_PrincipalId",
                table: "Dependents",
                column: "PrincipalId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Spouses_SpouseId",
                table: "Employees",
                column: "SpouseId",
                principalTable: "Spouses",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dependents_Employees_PrincipalId",
                table: "Dependents");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Spouses_SpouseId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Spouses",
                table: "Spouses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Dependents",
                table: "Dependents");

            migrationBuilder.DropIndex(
                name: "IX_Dependents_PrincipalId",
                table: "Dependents");

            migrationBuilder.DropColumn(
                name: "PrincipalId",
                table: "Dependents");

            migrationBuilder.RenameTable(
                name: "Spouses",
                newName: "Spouse");

            migrationBuilder.RenameTable(
                name: "Dependents",
                newName: "Dependent");

            migrationBuilder.AddColumn<Guid>(
                name: "EmployeeId",
                table: "Dependent",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Spouse",
                table: "Spouse",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Dependent",
                table: "Dependent",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Dependent_EmployeeId",
                table: "Dependent",
                column: "EmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dependent_Employees_EmployeeId",
                table: "Dependent",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Spouse_SpouseId",
                table: "Employees",
                column: "SpouseId",
                principalTable: "Spouse",
                principalColumn: "Id");
        }
    }
}
