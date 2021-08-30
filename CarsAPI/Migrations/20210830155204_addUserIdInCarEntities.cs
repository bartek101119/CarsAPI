using Microsoft.EntityFrameworkCore.Migrations;

namespace CarsAPI.Migrations
{
    public partial class addUserIdInCarEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CreateById",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedById",
                table: "Cars",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cars_CreatedById",
                table: "Cars",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Cars_Users_CreatedById",
                table: "Cars",
                column: "CreatedById",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cars_Users_CreatedById",
                table: "Cars");

            migrationBuilder.DropIndex(
                name: "IX_Cars_CreatedById",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreateById",
                table: "Cars");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "Cars");
        }
    }
}
