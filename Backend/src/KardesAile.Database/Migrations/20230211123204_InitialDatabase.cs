using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "kardesaile");

            migrationBuilder.CreateTable(
                name: "user",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    firstname = table.Column<string>(name: "first_name", type: "character varying(100)", maxLength: 100, nullable: false),
                    lastname = table.Column<string>(name: "last_name", type: "character varying(100)", maxLength: 100, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    emailvalidated = table.Column<bool>(name: "email_validated", type: "boolean", nullable: false),
                    phone = table.Column<string>(type: "character varying(30)", maxLength: 30, nullable: false),
                    phonevalidated = table.Column<bool>(name: "phone_validated", type: "boolean", nullable: false),
                    hash = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    salt = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    role = table.Column<int>(type: "integer", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_user", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "cocuk",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    dogumtarih = table.Column<DateTime>(name: "dogum_tarih", type: "timestamp with time zone", nullable: false),
                    cinsiyet = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_cocuk", x => x.id);
                    table.ForeignKey(
                        name: "fk_cocuk_user_user_id",
                        column: x => x.userid,
                        principalSchema: "kardesaile",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_cocuk_user_id",
                schema: "kardesaile",
                table: "cocuk",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_user_email",
                schema: "kardesaile",
                table: "user",
                column: "email",
                unique: true);
            
            migrationBuilder.InsertData(
                table: "user",
                columns: new[] { "id", "status", "first_name", "last_name", 
                    "email", "hash", "salt", "role", "created_by", "created_at", "email_validated", "phone_validated", "phone"},
                values: new object[]
                {
                    Guid.NewGuid(), 0, "Test", "Admin", "user@example.com",
                    "Uyk+qILQLc2HI/mfKLsmvuqSwuNqlUvyzyO/66oz0OI=", 
                    "/swMcEv4n+B3e4pWAZv2YcLxP02tiiOq4ufwcbyBC/I=",
                    0, "system", DateTime.UtcNow, false, false, "123-345"
                }, "kardesaile");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cocuk",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "user",
                schema: "kardesaile");
        }
    }
}
