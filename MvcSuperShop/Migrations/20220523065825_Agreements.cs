using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MvcSuperShop.Migrations
{
    public partial class Agreements : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidFrom = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ValidTo = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agreements", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgreementRow",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ManufacturerMatch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductMatch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CategoryMatch = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PercentageDiscount = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    AgreementId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgreementRow", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgreementRow_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "UserAgreements",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    AgreementId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAgreements", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserAgreements_Agreements_AgreementId",
                        column: x => x.AgreementId,
                        principalTable: "Agreements",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgreementRow_AgreementId",
                table: "AgreementRow",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreements_AgreementId",
                table: "UserAgreements",
                column: "AgreementId");

            migrationBuilder.CreateIndex(
                name: "IX_UserAgreements_Email",
                table: "UserAgreements",
                column: "Email");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgreementRow");

            migrationBuilder.DropTable(
                name: "UserAgreements");

            migrationBuilder.DropTable(
                name: "Agreements");
        }
    }
}
