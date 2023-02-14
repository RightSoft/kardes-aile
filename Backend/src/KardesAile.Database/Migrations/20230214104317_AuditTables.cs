using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AuditTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "audit",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    type = table.Column<int>(type: "integer", nullable: false),
                    action = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "audit_detail",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    auditid = table.Column<Guid>(name: "audit_id", type: "uuid", nullable: false),
                    entityname = table.Column<string>(name: "entity_name", type: "character varying(255)", maxLength: 255, nullable: false),
                    entityid = table.Column<Guid>(name: "entity_id", type: "uuid", nullable: false),
                    operation = table.Column<int>(type: "integer", nullable: false),
                    data = table.Column<string>(type: "json", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_detail", x => x.id);
                    table.ForeignKey(
                        name: "fk_audit_detail_audit_audit_id",
                        column: x => x.auditid,
                        principalSchema: "kardesaile",
                        principalTable: "audit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "audit_effected_user",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    auditid = table.Column<Guid>(name: "audit_id", type: "uuid", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_audit_effected_user", x => x.id);
                    table.ForeignKey(
                        name: "fk_audit_effected_user_audit_audit_id",
                        column: x => x.auditid,
                        principalSchema: "kardesaile",
                        principalTable: "audit",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_audit_effected_user_user_user_id",
                        column: x => x.userid,
                        principalSchema: "kardesaile",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_audit_detail_audit_id",
                schema: "kardesaile",
                table: "audit_detail",
                column: "audit_id");

            migrationBuilder.CreateIndex(
                name: "ix_audit_effected_user_audit_id",
                schema: "kardesaile",
                table: "audit_effected_user",
                column: "audit_id");

            migrationBuilder.CreateIndex(
                name: "ix_audit_effected_user_user_id",
                schema: "kardesaile",
                table: "audit_effected_user",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "audit_detail",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "audit_effected_user",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "audit",
                schema: "kardesaile");
        }
    }
}
