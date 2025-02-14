using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Backend.Domains.User.Persistence.Sql.Migrations
{
    /// <inheritdoc />
    public partial class CreatePermissionsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Key = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PermissionDescription",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Locale = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionDescription", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionDescription_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "User",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "PermissionName",
                schema: "User",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Locale = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    PermissionId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionName", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PermissionName_Permission_PermissionId",
                        column: x => x.PermissionId,
                        principalSchema: "User",
                        principalTable: "Permission",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDescription_Locale_PermissionId",
                schema: "User",
                table: "PermissionDescription",
                columns: new[] { "Locale", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionDescription_PermissionId",
                schema: "User",
                table: "PermissionDescription",
                column: "PermissionId");

            migrationBuilder.CreateIndex(
                name: "IX_PermissionName_Locale_PermissionId",
                schema: "User",
                table: "PermissionName",
                columns: new[] { "Locale", "PermissionId" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PermissionName_PermissionId",
                schema: "User",
                table: "PermissionName",
                column: "PermissionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PermissionDescription",
                schema: "User");

            migrationBuilder.DropTable(
                name: "PermissionName",
                schema: "User");

            migrationBuilder.DropTable(
                name: "Permission",
                schema: "User");
        }
    }
}
