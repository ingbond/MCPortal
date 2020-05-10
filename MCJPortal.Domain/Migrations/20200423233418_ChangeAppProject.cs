using Microsoft.EntityFrameworkCore.Migrations;

namespace MCJPortal.Domain.Migrations
{
    public partial class ChangeAppProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId1",
                table: "UserProjects");

            migrationBuilder.DropIndex(
                name: "IX_UserProjects_ProjectId1",
                table: "UserProjects");

            migrationBuilder.DropColumn(
                name: "ProjectId1",
                table: "UserProjects");
            
            migrationBuilder.AlterColumn<int>(
                name: "ProjectId",
                table: "UserProjects",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProjects",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProjects_Projects_ProjectId",
                table: "UserProjects");

            migrationBuilder.DropIndex(
                name: "IX_UserProjects_ProjectId",
                table: "UserProjects");
            
            migrationBuilder.AlterColumn<string>(
                name: "ProjectId",
                table: "UserProjects",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(int));

        }
    }
}
