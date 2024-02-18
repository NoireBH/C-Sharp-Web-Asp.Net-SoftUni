using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class noAction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
