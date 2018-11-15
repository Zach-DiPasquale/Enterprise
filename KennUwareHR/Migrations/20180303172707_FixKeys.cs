using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace KennUwareHR.Migrations
{
    public partial class FixKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authentications_Employees_EmployeeId",
                table: "Authentications");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Paychecks_Employees_EmployeeId",
                table: "Paychecks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_ReviewedEmpId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_ReviewerEmpId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewedEmpId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewerEmpId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewedEmpId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewerEmpId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewedEmployeeId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReviewerEmployeeId",
                table: "Reviews",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Paychecks",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Leaves",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Employees",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Authentications",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewedEmployeeId",
                table: "Reviews",
                column: "ReviewedEmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerEmployeeId",
                table: "Reviews",
                column: "ReviewerEmployeeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authentications_Employees_EmployeeId",
                table: "Authentications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Paychecks_Employees_EmployeeId",
                table: "Paychecks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_ReviewedEmployeeId",
                table: "Reviews",
                column: "ReviewedEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_ReviewerEmployeeId",
                table: "Reviews",
                column: "ReviewerEmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Authentications_Employees_EmployeeId",
                table: "Authentications");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves");

            migrationBuilder.DropForeignKey(
                name: "FK_Paychecks_Employees_EmployeeId",
                table: "Paychecks");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_ReviewedEmployeeId",
                table: "Reviews");

            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Employees_ReviewerEmployeeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewedEmployeeId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_ReviewerEmployeeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewedEmployeeId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "ReviewerEmployeeId",
                table: "Reviews");

            migrationBuilder.AddColumn<int>(
                name: "ReviewedEmpId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ReviewerEmpId",
                table: "Reviews",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Paychecks",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Leaves",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "RoleId",
                table: "Employees",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "EmployeeId",
                table: "Authentications",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewedEmpId",
                table: "Reviews",
                column: "ReviewedEmpId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ReviewerEmpId",
                table: "Reviews",
                column: "ReviewerEmpId");

            migrationBuilder.AddForeignKey(
                name: "FK_Authentications_Employees_EmployeeId",
                table: "Authentications",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Roles_RoleId",
                table: "Employees",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Leaves_Employees_EmployeeId",
                table: "Leaves",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Paychecks_Employees_EmployeeId",
                table: "Paychecks",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_ReviewedEmpId",
                table: "Reviews",
                column: "ReviewedEmpId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Employees_ReviewerEmpId",
                table: "Reviews",
                column: "ReviewerEmpId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
