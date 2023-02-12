using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddModerators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "moderator",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    fullname = table.Column<string>(name: "full_name", type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    password = table.Column<string>(type: "character varying(8)", maxLength: 8, nullable: false),
                    isdeleted = table.Column<bool>(name: "is_deleted", type: "boolean", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_moderator", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_moderator_email",
                schema: "kardesaile",
                table: "moderator",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "moderator",
                schema: "kardesaile");
        }
    }
}
