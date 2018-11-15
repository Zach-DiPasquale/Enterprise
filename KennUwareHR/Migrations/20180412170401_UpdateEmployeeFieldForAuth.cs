using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KennUwareHR.Migrations
{
    public partial class UpdateEmployeeFieldForAuth : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Employee",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MyManagerId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Employees",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Employee",
                table: "Employees",
                column: "Employee");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Employees_Employee",
                table: "Employees",
                column: "Employee",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Employees_Employee",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_Employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "Employee",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "MyManagerId",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Employees");
        }
    }
}
