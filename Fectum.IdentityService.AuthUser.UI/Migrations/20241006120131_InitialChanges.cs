using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fectum.Identity.AuthService.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialChanges : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConfirmPassword",
                table: "IUsersInfoRegistrations");

            migrationBuilder.CreateTable(
                name: "IUserInfoTechnologyDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Technologies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUserInfoTechnologyDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IUserInfoTechnologyDetails_IUsersInformation_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsersInformation",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IUserInfoTechnologyDetails_UserId",
                table: "IUserInfoTechnologyDetails",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IUserInfoTechnologyDetails");

            migrationBuilder.AddColumn<string>(
                name: "ConfirmPassword",
                table: "IUsersInfoRegistrations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
