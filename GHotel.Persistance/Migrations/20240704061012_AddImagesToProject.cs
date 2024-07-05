using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHotel.Persistance.Migrations
{
    public partial class AddImagesToProject : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Url = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Url);
                    table.ForeignKey(
                        name: "FK_Images_Projects_ProjectName",
                        column: x => x.ProjectName,
                        principalTable: "Projects",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProjectName",
                table: "Images",
                column: "ProjectName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Images");
        }
    }
}
