using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHotel.Persistance.Migrations
{
    public partial class AddShares : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shares",
                columns: table => new
                {
                    UserPersonalNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ProjectName = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    SharePercentage = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shares", x => new { x.UserPersonalNumber, x.ProjectName });
                    table.ForeignKey(
                        name: "FK_Shares_Projects_ProjectName",
                        column: x => x.ProjectName,
                        principalTable: "Projects",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Shares_Users_UserPersonalNumber",
                        column: x => x.UserPersonalNumber,
                        principalTable: "Users",
                        principalColumn: "PersonalNumber",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shares_ProjectName",
                table: "Shares",
                column: "ProjectName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shares");
        }
    }
}
