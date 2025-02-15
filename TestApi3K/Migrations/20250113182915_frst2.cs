using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaDigestApi.Migrations
{
    /// <inheritdoc />
    public partial class frst2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "name",
                table: "Role",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "name",
                table: "Role");
        }
    }
}
