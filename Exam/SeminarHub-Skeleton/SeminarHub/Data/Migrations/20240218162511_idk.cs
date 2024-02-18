using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SeminarHub.Data.Migrations
{
    public partial class idk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                table: "SeminarsParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                table: "SeminarsParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                table: "SeminarsParticipants");

            migrationBuilder.DropForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants");

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_AspNetUsers_ParticipantId",
                table: "SeminarsParticipants",
                column: "ParticipantId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SeminarsParticipants_Seminars_SeminarId",
                table: "SeminarsParticipants",
                column: "SeminarId",
                principalTable: "Seminars",
                principalColumn: "Id");
        }
    }
}
