using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsAPI.Migrations
{
    public partial class addCarCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CarCompanyId",
                table: "Cars",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CarCompany",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LegalForm = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    REGON = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NIP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfCommencementOfActivity = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarCompany", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CarCompanyId",
                table: "Cars",
                column: "CarCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_CarCompany_CarCompanyId",
                table: "Cars",
                column: "CarCompanyId",
                principalTable: "CarCompany",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_CarCompany_CarCompanyId",
                table: "Cars");

            migrationBuilder.DropTable(
                name: "CarCompany");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CarCompanyId",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CarCompanyId",
                table: "Cars");
        }
    }
}
