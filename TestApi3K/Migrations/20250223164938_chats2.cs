using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDigestApi.Migrations
{
    /// <inheritdoc />
    public partial class chats2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_firsUserId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Users_firsUserId",
                table: "UserChats",
                column: "firsUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats",
                column: "secondUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_firsUserId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats");

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Users_firsUserId",
                table: "UserChats",
                column: "firsUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats",
                column: "secondUserId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
