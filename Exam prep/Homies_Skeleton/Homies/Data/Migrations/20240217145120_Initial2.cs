using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Homies.Data.Migrations
{
    public partial class Initial2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "OrganizerId",
                table: "Events",
                newName: "OrganiserId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganizerId",
                table: "Events",
                newName: "IX_Events_OrganiserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events",
                column: "OrganiserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_AspNetUsers_OrganiserId",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "OrganiserId",
                table: "Events",
                newName: "OrganizerId");

            migrationBuilder.RenameIndex(
                name: "IX_Events_OrganiserId",
                table: "Events",
                newName: "IX_Events_OrganizerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_AspNetUsers_OrganizerId",
                table: "Events",
                column: "OrganizerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
