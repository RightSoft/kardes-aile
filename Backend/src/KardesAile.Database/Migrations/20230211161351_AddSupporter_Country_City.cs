using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddSupporterCountryCity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cocuk",
                schema: "kardesaile");

            migrationBuilder.CreateTable(
                name: "child",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    birthdate = table.Column<DateOnly>(name: "birth_date", type: "date", nullable: false),
                    gender = table.Column<int>(type: "integer", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_child", x => x.id);
                    table.ForeignKey(
                        name: "fk_child_user_user_id",
                        column: x => x.userid,
                        principalSchema: "kardesaile",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "country",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_country", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "city",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false),
                    countryid = table.Column<Guid>(name: "country_id", type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_city", x => x.id);
                    table.ForeignKey(
                        name: "fk_city_country_country_id",
                        column: x => x.countryid,
                        principalSchema: "kardesaile",
                        principalTable: "country",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "supporter",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    countryid = table.Column<Guid>(name: "country_id", type: "uuid", nullable: true),
                    cityid = table.Column<Guid>(name: "city_id", type: "uuid", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_supporter", x => x.id);
                    table.ForeignKey(
                        name: "fk_supporter_city_city_id",
                        column: x => x.cityid,
                        principalSchema: "kardesaile",
                        principalTable: "city",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_supporter_country_country_id",
                        column: x => x.countryid,
                        principalSchema: "kardesaile",
                        principalTable: "country",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_supporter_user_user_id",
                        column: x => x.userid,
                        principalSchema: "kardesaile",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_child_user_id",
                schema: "kardesaile",
                table: "child",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_country_id",
                schema: "kardesaile",
                table: "city",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_city_name",
                schema: "kardesaile",
                table: "city",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_country_name",
                schema: "kardesaile",
                table: "country",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_supporter_city_id",
                schema: "kardesaile",
                table: "supporter",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_supporter_country_id",
                schema: "kardesaile",
                table: "supporter",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_supporter_user_id",
                schema: "kardesaile",
                table: "supporter",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "child",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "supporter",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "city",
                schema: "kardesaile");

            migrationBuilder.DropTable(
                name: "country",
                schema: "kardesaile");

            migrationBuilder.CreateTable(
                name: "cocuk",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    ad = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    cinsiyet = table.Column<int>(type: "integer", nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    dogumtarih = table.Column<DateTime>(name: "dogum_tarih", type: "timestamp with time zone", nullable: false),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
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
        }
    }
}
