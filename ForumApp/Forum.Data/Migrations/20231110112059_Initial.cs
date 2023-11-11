using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ForumApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Posts",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1500)", maxLength: 1500, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Posts", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Posts",
                columns: new[] { "Id", "Content", "Title" },
                values: new object[,]
                {
                    { new Guid("2470ad86-432c-4cdb-8f38-b37c7e19b588"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer facilisis nisi non elementum vehicula. Nunc scelerisque erat urna, at mattis.", "my first post" },
                    { new Guid("36a2e6ac-4d0e-4451-ba65-4fec4ed79528"), "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Curabitur pharetra erat ante, ac finibus massa sodales vehicula. Ut iaculis tortor quis imperdiet imperdiet. In euismod.", "my second post" },
                    { new Guid("a8d56cb9-1382-4b6f-b145-61401b0310a5"), "Translations: Can you help translate this site into a foreign language ? Please email us with details if you can help.", "my third post" },
                    { new Guid("ce96ef2a-6ca3-4415-a1b7-868962b900e7"), "Section 1.10.32 of \"de Finibus Bonorum et Malorum\", written by Cicero in 45 BC\r\n\"Sed ut perspiciatis unde omnis iste natus error sit voluptatem accusantium doloremque laudantium, totam rem aperiam, eaque ipsa quae ab illo inventore veritatis et quasi architecto beatae vitae dicta sunt explicabo. Nemo enim ipsam voluptatem quia voluptas sit aspernatur aut odit aut fugit, sed quia consequuntur magni dolores eos qui ratione voluptatem sequi nesciunt. ", "my fourth post" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Posts");
        }
    }
}
