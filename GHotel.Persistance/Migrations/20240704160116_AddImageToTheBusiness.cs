using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GHotel.Persistance.Migrations
{
    public partial class AddImageToTheBusiness : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageUrl",
                table: "Businesses",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Businesses_ImageUrl",
                table: "Businesses",
                column: "ImageUrl",
                unique: true,
                filter: "[ImageUrl] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Businesses_Images_ImageUrl",
                table: "Businesses",
                column: "ImageUrl",
                principalTable: "Images",
                principalColumn: "Url",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Businesses_Images_ImageUrl",
                table: "Businesses");

            migrationBuilder.DropIndex(
                name: "IX_Businesses_ImageUrl",
                table: "Businesses");

            migrationBuilder.DropColumn(
                name: "ImageUrl",
                table: "Businesses");
        }
    }
}
