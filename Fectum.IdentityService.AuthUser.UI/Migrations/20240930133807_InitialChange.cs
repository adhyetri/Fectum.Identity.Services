using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Fectum.Identity.AuthService.Api.Migrations
{
    /// <inheritdoc />
    public partial class InitialChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "IUsersInfoRole",
                columns: table => new
                {
                    RoleId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUsersInfoRole", x => x.RoleId);
                });

            migrationBuilder.CreateTable(
                name: "IUsersInfoRegistrations",
                columns: table => new
                {
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmailAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ConfirmPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RoleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUsersInfoRegistrations", x => x.RegistrationId);
                    table.ForeignKey(
                        name: "FK_IUsersInfoRegistrations_IUsersInfoRole_RoleId",
                        column: x => x.RoleId,
                        principalTable: "IUsersInfoRole",
                        principalColumn: "RoleId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUsersInformation",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MobileNumber = table.Column<long>(type: "bigint", nullable: false),
                    AadharNumber = table.Column<long>(type: "bigint", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RegistrationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUsersInformation", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_IUsersInformation_IUsersInfoRegistrations_RegistrationId",
                        column: x => x.RegistrationId,
                        principalTable: "IUsersInfoRegistrations",
                        principalColumn: "RegistrationId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUsersInfoAddress",
                columns: table => new
                {
                    AddressId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AddressType = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<int>(type: "int", nullable: false),
                    StreetAddress = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StateName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    IsSameAsPermanent = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUsersInfoAddress", x => x.AddressId);
                    table.ForeignKey(
                        name: "FK_IUsersInfoAddress_IUsersInformation_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsersInformation",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IUsersInfoEducation",
                columns: table => new
                {
                    EducationId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DegreeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UniversityName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CompletionYear = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IUsersInfoEducation", x => x.EducationId);
                    table.ForeignKey(
                        name: "FK_IUsersInfoEducation_IUsersInformation_UserId",
                        column: x => x.UserId,
                        principalTable: "IUsersInformation",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_IUsersInfoAddress_UserId",
                table: "IUsersInfoAddress",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IUsersInfoEducation_UserId",
                table: "IUsersInfoEducation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_IUsersInfoRegistrations_RoleId",
                table: "IUsersInfoRegistrations",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_IUsersInformation_RegistrationId",
                table: "IUsersInformation",
                column: "RegistrationId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "IUsersInfoAddress");

            migrationBuilder.DropTable(
                name: "IUsersInfoEducation");

            migrationBuilder.DropTable(
                name: "IUsersInformation");

            migrationBuilder.DropTable(
                name: "IUsersInfoRegistrations");

            migrationBuilder.DropTable(
                name: "IUsersInfoRole");
        }
    }
}
