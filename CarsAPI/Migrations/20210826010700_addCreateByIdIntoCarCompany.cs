using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsAPI.Migrations
{
    public partial class addCreateByIdIntoCarCompany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "CarCompany",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "CarCompany",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CarCompany_CreatedById",
                table: "CarCompany",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_CarCompany_Users_CreatedById",
                table: "CarCompany",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarCompany_Users_CreatedById",
                table: "CarCompany");

            migrationBuilder.DropIndex(
                name: "IX_CarCompany_CreatedById",
                table: "CarCompany");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "CarCompany");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "CarCompany");
        }
    }
}
