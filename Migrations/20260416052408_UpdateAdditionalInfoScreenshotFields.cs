using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StudentManagementSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateAdditionalInfoScreenshotFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TaxID",
                table: "AdditionalInfo",
                newName: "TaxPayerName");

            migrationBuilder.RenameColumn(
                name: "PreviousSchool",
                table: "AdditionalInfo",
                newName: "TaxPayerCity");

            migrationBuilder.RenameColumn(
                name: "GuardianPhone",
                table: "AdditionalInfo",
                newName: "GuardianMobileNo");

            migrationBuilder.AddColumn<string>(
                name: "ActiveTaxPayer",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Behaviour",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Business",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Certificates",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianAddress",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GuardianEmail",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NIC",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NTN",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Performance",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReasonForLeaving",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "SchoolLeavingDate",
                table: "AdditionalInfo",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TaxPayerAddress",
                table: "AdditionalInfo",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveTaxPayer",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Behaviour",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Business",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Certificates",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "GuardianAddress",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "GuardianEmail",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "NIC",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "NTN",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "Performance",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "ReasonForLeaving",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "SchoolLeavingDate",
                table: "AdditionalInfo");

            migrationBuilder.DropColumn(
                name: "TaxPayerAddress",
                table: "AdditionalInfo");

            migrationBuilder.RenameColumn(
                name: "TaxPayerName",
                table: "AdditionalInfo",
                newName: "TaxID");

            migrationBuilder.RenameColumn(
                name: "TaxPayerCity",
                table: "AdditionalInfo",
                newName: "PreviousSchool");

            migrationBuilder.RenameColumn(
                name: "GuardianMobileNo",
                table: "AdditionalInfo",
                newName: "GuardianPhone");
        }
    }
}
