using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.User.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddUserPermissionRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionEntityUserEntity",
                schema: "User",
                columns: table => new
                {
                    PermissionsId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    UsersId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionEntityUserEntity", x => new { x.PermissionsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_PermissionEntityUserEntity_Permission_PermissionsId",
                        column: x => x.PermissionsId,
                        principalSchema: "User",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PermissionEntityUserEntity_Users_UsersId",
                        column: x => x.UsersId,
                        principalSchema: "User",
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionEntityUserEntity_UsersId",
                schema: "User",
                table: "PermissionEntityUserEntity",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionEntityUserEntity",
                schema: "User");
        }
    }
}
