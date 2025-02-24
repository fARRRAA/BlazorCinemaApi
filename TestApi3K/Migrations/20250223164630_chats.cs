using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDigestApi.Migrations
{
    /// <inheritdoc />
    public partial class chats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MovieChats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    movieId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieChats", x => x.id);
                    table.ForeignKey(
                        name: "FK_MovieChats_Movies_movieId",
                        column: x => x.movieId,
                        principalTable: "Movies",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChats",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firsUserId = table.Column<int>(type: "int", nullable: false),
                    secondUserId = table.Column<int>(type: "int", nullable: false),
                    created_at = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChats", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_firsUserId",
                        column: x => x.firsUserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChats_Users_secondUserId",
                        column: x => x.secondUserId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MovieChatsMessages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    photoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieChatsMessages", x => x.id);
                    table.ForeignKey(
                        name: "FK_MovieChatsMessages_MovieChats_chatId",
                        column: x => x.chatId,
                        principalTable: "MovieChats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieChatsMessages_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserChatMesaages",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    chatId = table.Column<int>(type: "int", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false),
                    message = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    sent_at = table.Column<DateTime>(type: "datetime2", nullable: false),
                    photoUrl = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatMesaages", x => x.id);
                    table.ForeignKey(
                        name: "FK_UserChatMesaages_UserChats_chatId",
                        column: x => x.chatId,
                        principalTable: "UserChats",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserChatMesaages_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieChats_movieId",
                table: "MovieChats",
                column: "movieId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieChatsMessages_chatId",
                table: "MovieChatsMessages",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieChatsMessages_userId",
                table: "MovieChatsMessages",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatMesaages_chatId",
                table: "UserChatMesaages",
                column: "chatId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChatMesaages_userId",
                table: "UserChatMesaages",
                column: "userId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_firsUserId",
                table: "UserChats",
                column: "firsUserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserChats_secondUserId",
                table: "UserChats",
                column: "secondUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieChatsMessages");

            migrationBuilder.DropTable(
                name: "UserChatMesaages");

            migrationBuilder.DropTable(
                name: "MovieChats");

            migrationBuilder.DropTable(
                name: "UserChats");
        }
    }
}
