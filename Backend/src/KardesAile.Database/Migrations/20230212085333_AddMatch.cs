using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMatch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "match",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    supporterid = table.Column<Guid>(name: "supporter_id", type: "uuid", nullable: false),
                    victimid = table.Column<Guid>(name: "victim_id", type: "uuid", nullable: false),
                    supporterchildid = table.Column<Guid>(name: "supporter_child_id", type: "uuid", nullable: true),
                    victimchildid = table.Column<Guid>(name: "victim_child_id", type: "uuid", nullable: true),
                    active = table.Column<bool>(type: "boolean", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_match", x => x.id);
                    table.ForeignKey(
                        name: "fk_match_child_supporter_child_id",
                        column: x => x.supporterchildid,
                        principalSchema: "kardesaile",
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_match_child_victim_child_id",
                        column: x => x.victimchildid,
                        principalSchema: "kardesaile",
                        principalTable: "child",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_match_supporter_supporter_id",
                        column: x => x.supporterid,
                        principalSchema: "kardesaile",
                        principalTable: "supporter",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_match_supporter_child_id",
                schema: "kardesaile",
                table: "match",
                column: "supporter_child_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_match_supporter_id",
                schema: "kardesaile",
                table: "match",
                column: "supporter_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_match_victim_child_id",
                schema: "kardesaile",
                table: "match",
                column: "victim_child_id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "match",
                schema: "kardesaile");
        }
    }
}
