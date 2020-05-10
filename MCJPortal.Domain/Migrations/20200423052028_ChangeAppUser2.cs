using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MCJPortal.Domain.Migrations
{
    public partial class ChangeAppUser2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAllProjectsAllowed",
                table: "Users",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationId",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "UserRoles",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");
                                    

            migrationBuilder.CreateIndex(
                name: "IX_Users_LocationId",
                table: "Users",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ListItems_ListTypeId",
                table: "ListItems",
                column: "ListTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_ListItems_LocationId",
                table: "Users",
                column: "LocationId",
                principalTable: "ListItems",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_ListItems_LocationId",
                table: "Users");
                      
            migrationBuilder.DropIndex(
                name: "IX_Users_LocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "IsAllProjectsAllowed",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "UserRoles");
        }
    }
}
