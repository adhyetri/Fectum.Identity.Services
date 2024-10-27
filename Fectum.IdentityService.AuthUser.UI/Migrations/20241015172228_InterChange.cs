using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fectum.Identity.AuthService.Api.Migrations
{
    /// <inheritdoc />
    public partial class InterChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IUsersInfoRegistrations_IUsersInfoRole_RoleId",
                table: "IUsersInfoRegistrations");

            migrationBuilder.DropIndex(
                name: "IX_IUsersInfoRegistrations_RoleId",
                table: "IUsersInfoRegistrations");

            migrationBuilder.DropColumn(
                name: "RoleId",
                table: "IUsersInfoRegistrations");

            migrationBuilder.AlterColumn<int>(
                name: "RoleName",
                table: "IUsersInfoRole",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<bool>(
                name: "IsWorkingProfessional",
                table: "IUsersInformation",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "RoleName",
                table: "IUsersInformation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWorkingProfessional",
                table: "IUsersInformation");

            migrationBuilder.DropColumn(
                name: "RoleName",
                table: "IUsersInformation");

            migrationBuilder.AlterColumn<string>(
                name: "RoleName",
                table: "IUsersInfoRole",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "RoleId",
                table: "IUsersInfoRegistrations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_IUsersInfoRegistrations_RoleId",
                table: "IUsersInfoRegistrations",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_IUsersInfoRegistrations_IUsersInfoRole_RoleId",
                table: "IUsersInfoRegistrations",
                column: "RoleId",
                principalTable: "IUsersInfoRole",
                principalColumn: "RoleId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
