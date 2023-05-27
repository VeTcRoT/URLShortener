using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UrlShortener.Migrations
{
    /// <inheritdoc />
    public partial class AddUserColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "UrlDatas",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_UrlDatas_AspNetUsers_UserId",
                table: "UrlDatas",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UrlDatas_AspNetUsers_UserId",
                table: "UrlDatas");

            migrationBuilder.DropIndex(
                name: "IX_UrlDatas_UserId",
                table: "UrlDatas");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "UrlDatas");
        }
    }
}
