using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdmissionModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AdmissionDate",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "AdmissionNo",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "ClassName",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "TestScore",
                table: "Admissions");

            migrationBuilder.RenameColumn(
                name: "TestResult",
                table: "Admissions",
                newName: "TestCleared");

            migrationBuilder.AddColumn<string>(
                name: "ActiveInClass",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ConductedBy",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DbStatus",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RequestedClass",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RollNo",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Section",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Session",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "TestDate",
                table: "Admissions",
                type: "datetime2",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveInClass",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "ConductedBy",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "DbStatus",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "RequestedClass",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "RollNo",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "Section",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "Session",
                table: "Admissions");

            migrationBuilder.DropColumn(
                name: "TestDate",
                table: "Admissions");

            migrationBuilder.RenameColumn(
                name: "TestCleared",
                table: "Admissions",
                newName: "TestResult");

            migrationBuilder.AddColumn<DateTime>(
                name: "AdmissionDate",
                table: "Admissions",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "AdmissionNo",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ClassName",
                table: "Admissions",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "TestScore",
                table: "Admissions",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
