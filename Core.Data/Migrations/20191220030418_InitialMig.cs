using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Core.Data.Migrations
{
    public partial class InitialMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Owners",
                columns: table => new
                {
                    OwnerId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(maxLength: 60, nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Address = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Owners", x => x.OwnerId);
                });

            migrationBuilder.CreateTable(
                name: "RecordTypes",
                columns: table => new
                {
                    ID = table.Column<string>(nullable: false),
                    PageID = table.Column<string>(nullable: true),
                    SectionID = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    XPathRoot = table.Column<string>(nullable: true),
                    PKColumn = table.Column<string>(nullable: true),
                    IdentifierExpression = table.Column<string>(nullable: true),
                    RepeatableInd = table.Column<bool>(nullable: false),
                    Category = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordTypes", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "validationErrors",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RecordTypeID = table.Column<string>(nullable: true),
                    FieldID = table.Column<string>(nullable: true),
                    ValidationID = table.Column<decimal>(nullable: false),
                    ApplicationID = table.Column<int>(nullable: false),
                    CurrentValue = table.Column<string>(nullable: true),
                    PKValue = table.Column<int>(nullable: true),
                    SectionName = table.Column<string>(nullable: true),
                    Severity = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    GroupID = table.Column<string>(nullable: true),
                    Identifier = table.Column<string>(nullable: true),
                    AssociatedTabID = table.Column<string>(nullable: true),
                    FocusableInd = table.Column<bool>(nullable: false),
                    HighlightControlID = table.Column<string>(nullable: true),
                    FocusControlID = table.Column<string>(nullable: true),
                    ControlType = table.Column<string>(nullable: true),
                    RepeatableInd = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_validationErrors", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Validations",
                columns: table => new
                {
                    ID = table.Column<decimal>(nullable: false),
                    SpectrumEditID = table.Column<string>(nullable: true),
                    GroupID = table.Column<string>(nullable: true),
                    SequenceNo = table.Column<byte>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    RecordTypeID = table.Column<string>(nullable: true),
                    FieldID = table.Column<string>(nullable: true),
                    Label = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    ErrorMessage = table.Column<string>(nullable: true),
                    Condition = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: false),
                    Type = table.Column<byte>(nullable: false),
                    Severity = table.Column<string>(nullable: true),
                    XPath = table.Column<string>(nullable: true),
                    RegularExpression = table.Column<string>(nullable: true),
                    LookupID = table.Column<string>(nullable: true),
                    ComparisonFieldXPath = table.Column<string>(nullable: true),
                    RangeFromValue = table.Column<decimal>(nullable: true),
                    RangeToValue = table.Column<decimal>(nullable: true),
                    FunctionName = table.Column<string>(nullable: true),
                    AssemblyName = table.Column<string>(nullable: true),
                    SectionName = table.Column<string>(nullable: true),
                    XPathExpression = table.Column<string>(nullable: true),
                    AssociatedTabID = table.Column<string>(nullable: true),
                    HighlightControlID = table.Column<string>(nullable: true),
                    FocusControlID = table.Column<string>(nullable: true),
                    FocusableInd = table.Column<bool>(nullable: false),
                    DefaultValue = table.Column<string>(nullable: true),
                    ControlType = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Validations", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    AccountId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    AccountType = table.Column<string>(nullable: false),
                    OwnerId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.AccountId);
                    table.ForeignKey(
                        name: "FK_Accounts_Owners_OwnerId",
                        column: x => x.OwnerId,
                        principalTable: "Owners",
                        principalColumn: "OwnerId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ValidationParameters",
                columns: table => new
                {
                    ValidationID = table.Column<decimal>(nullable: false),
                    SequenceNo = table.Column<byte>(nullable: false),
                    XPath = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    ValidationID1 = table.Column<decimal>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ValidationParameters", x => x.ValidationID);
                    table.ForeignKey(
                        name: "FK_ValidationParameters_Validations_ValidationID1",
                        column: x => x.ValidationID1,
                        principalTable: "Validations",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_OwnerId",
                table: "Accounts",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_ValidationParameters_ValidationID1",
                table: "ValidationParameters",
                column: "ValidationID1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "RecordTypes");

            migrationBuilder.DropTable(
                name: "validationErrors");

            migrationBuilder.DropTable(
                name: "ValidationParameters");

            migrationBuilder.DropTable(
                name: "Owners");

            migrationBuilder.DropTable(
                name: "Validations");
        }
    }
}
