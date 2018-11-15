using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KennUwareHR.Migrations
{
    public partial class FixContext : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departmetns_DepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departmetns",
                table: "Departmetns");

            migrationBuilder.RenameTable(
                name: "Departmetns",
                newName: "Departments");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departments",
                table: "Departments",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Departments_DepartmentId",
                table: "Employees");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Departments",
                table: "Departments");

            migrationBuilder.RenameTable(
                name: "Departments",
                newName: "Departmetns");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Departmetns",
                table: "Departmetns",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Departmetns_DepartmentId",
                table: "Employees",
                column: "DepartmentId",
                principalTable: "Departmetns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
