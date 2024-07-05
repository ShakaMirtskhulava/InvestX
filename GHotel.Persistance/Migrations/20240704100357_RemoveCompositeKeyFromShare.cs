using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHotel.Persistance.Migrations
{
    public partial class RemoveCompositeKeyFromShare : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shares",
                table: "Shares");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Shares",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Shares_UserPersonalNumber",
                table: "Shares",
                column: "UserPersonalNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Shares",
                table: "Shares");

            migrationBuilder.DropIndex(
                name: "IX_Shares_UserPersonalNumber",
                table: "Shares");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Shares");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Shares",
                table: "Shares",
                columns: new[] { "UserPersonalNumber", "ProjectName" });
        }
    }
}
