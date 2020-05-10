using Microsoft.EntityFrameworkCore.Migrations;

namespace MCJPortal.Domain.Migrations
{
    public partial class RolePermission : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Users_UserId",
                table: "UserProjects",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
