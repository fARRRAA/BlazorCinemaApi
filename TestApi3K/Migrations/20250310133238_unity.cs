using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDigestApi.Migrations
{
    /// <inheritdoc />
    public partial class unity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieChatsMessages_MovieChats_chatId",
                table: "MovieChatsMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieChatsMessages_Users_userId",
                table: "MovieChatsMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMesaages_UserChats_chatId",
                table: "UserChatMesaages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMesaages_Users_userId",
                table: "UserChatMesaages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_firsUserId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChatMesaages",
                table: "UserChatMesaages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieChatsMessages",
                table: "MovieChatsMessages");

            migrationBuilder.RenameTable(
                name: "UserChatMesaages",
                newName: "UserChatMessages");

            migrationBuilder.RenameTable(
                name: "MovieChatsMessages",
                newName: "MovieChatMessages");

            migrationBuilder.RenameColumn(
                name: "firsUserId",
                table: "UserChats",
                newName: "firstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChats_firsUserId",
                table: "UserChats",
                newName: "IX_UserChats_firstUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChatMesaages_userId",
                table: "UserChatMessages",
                newName: "IX_UserChatMessages_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChatMesaages_chatId",
                table: "UserChatMessages",
                newName: "IX_UserChatMessages_chatId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieChatsMessages_userId",
                table: "MovieChatMessages",
                newName: "IX_MovieChatMessages_userId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieChatsMessages_chatId",
                table: "MovieChatMessages",
                newName: "IX_MovieChatMessages_chatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChatMessages",
                table: "UserChatMessages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieChatMessages",
                table: "MovieChatMessages",
                column: "id");

            migrationBuilder.CreateTable(
                name: "UnityUsers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    login = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    coins = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnityUsers", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_MovieChatMessages_MovieChats_chatId",
                table: "MovieChatMessages",
                column: "chatId",
                principalTable: "MovieChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieChatMessages_Users_userId",
                table: "MovieChatMessages",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMessages_UserChats_chatId",
                table: "UserChatMessages",
                column: "chatId",
                principalTable: "UserChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMessages_Users_userId",
                table: "UserChatMessages",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChats_Users_firstUserId",
                table: "UserChats",
                column: "firstUserId",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovieChatMessages_MovieChats_chatId",
                table: "MovieChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_MovieChatMessages_Users_userId",
                table: "MovieChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMessages_UserChats_chatId",
                table: "UserChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChatMessages_Users_userId",
                table: "UserChatMessages");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_firstUserId",
                table: "UserChats");

            migrationBuilder.DropForeignKey(
                name: "FK_UserChats_Users_secondUserId",
                table: "UserChats");

            migrationBuilder.DropTable(
                name: "UnityUsers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserChatMessages",
                table: "UserChatMessages");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MovieChatMessages",
                table: "MovieChatMessages");

            migrationBuilder.RenameTable(
                name: "UserChatMessages",
                newName: "UserChatMesaages");

            migrationBuilder.RenameTable(
                name: "MovieChatMessages",
                newName: "MovieChatsMessages");

            migrationBuilder.RenameColumn(
                name: "firstUserId",
                table: "UserChats",
                newName: "firsUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChats_firstUserId",
                table: "UserChats",
                newName: "IX_UserChats_firsUserId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChatMessages_userId",
                table: "UserChatMesaages",
                newName: "IX_UserChatMesaages_userId");

            migrationBuilder.RenameIndex(
                name: "IX_UserChatMessages_chatId",
                table: "UserChatMesaages",
                newName: "IX_UserChatMesaages_chatId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieChatMessages_userId",
                table: "MovieChatsMessages",
                newName: "IX_MovieChatsMessages_userId");

            migrationBuilder.RenameIndex(
                name: "IX_MovieChatMessages_chatId",
                table: "MovieChatsMessages",
                newName: "IX_MovieChatsMessages_chatId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserChatMesaages",
                table: "UserChatMesaages",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MovieChatsMessages",
                table: "MovieChatsMessages",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_MovieChatsMessages_MovieChats_chatId",
                table: "MovieChatsMessages",
                column: "chatId",
                principalTable: "MovieChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MovieChatsMessages_Users_userId",
                table: "MovieChatsMessages",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMesaages_UserChats_chatId",
                table: "UserChatMesaages",
                column: "chatId",
                principalTable: "UserChats",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserChatMesaages_Users_userId",
                table: "UserChatMesaages",
                column: "userId",
                principalTable: "Users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

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
    }
}
