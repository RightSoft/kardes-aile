using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddDisasterVictim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "disaster_victim",
                schema: "kardesaile",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    address = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    addressvalidated = table.Column<bool>(name: "address_validated", type: "boolean", nullable: false),
                    temporaryaddress = table.Column<string>(name: "temporary_address", type: "character varying(255)", maxLength: 255, nullable: true),
                    userid = table.Column<Guid>(name: "user_id", type: "uuid", nullable: false),
                    countryid = table.Column<Guid>(name: "country_id", type: "uuid", nullable: true),
                    cityid = table.Column<Guid>(name: "city_id", type: "uuid", nullable: true),
                    temporarycityid = table.Column<Guid>(name: "temporary_city_id", type: "uuid", nullable: true),
                    identitynumber = table.Column<string>(name: "identity_number", type: "character(11)", fixedLength: true, maxLength: 11, nullable: true),
                    identitynumbervalidated = table.Column<bool>(name: "identity_number_validated", type: "boolean", nullable: false),
                    modifiedby = table.Column<string>(name: "modified_by", type: "character varying(255)", maxLength: 255, nullable: true),
                    modifiedat = table.Column<DateTime>(name: "modified_at", type: "timestamp with time zone", nullable: true),
                    createdby = table.Column<string>(name: "created_by", type: "character varying(255)", maxLength: 255, nullable: false),
                    createdat = table.Column<DateTime>(name: "created_at", type: "timestamp with time zone", nullable: false),
                    xmin = table.Column<uint>(type: "xid", rowVersion: true, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_disaster_victim", x => x.id);
                    table.ForeignKey(
                        name: "fk_disaster_victim_city_city_id",
                        column: x => x.cityid,
                        principalSchema: "kardesaile",
                        principalTable: "city",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_disaster_victim_city_temporary_city_id",
                        column: x => x.temporarycityid,
                        principalSchema: "kardesaile",
                        principalTable: "city",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_disaster_victim_country_country_id",
                        column: x => x.countryid,
                        principalSchema: "kardesaile",
                        principalTable: "country",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_disaster_victim_user_user_id",
                        column: x => x.userid,
                        principalSchema: "kardesaile",
                        principalTable: "user",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_disaster_victim_city_id",
                schema: "kardesaile",
                table: "disaster_victim",
                column: "city_id");

            migrationBuilder.CreateIndex(
                name: "ix_disaster_victim_country_id",
                schema: "kardesaile",
                table: "disaster_victim",
                column: "country_id");

            migrationBuilder.CreateIndex(
                name: "ix_disaster_victim_temporary_city_id",
                schema: "kardesaile",
                table: "disaster_victim",
                column: "temporary_city_id");

            migrationBuilder.CreateIndex(
                name: "ix_disaster_victim_user_id",
                schema: "kardesaile",
                table: "disaster_victim",
                column: "user_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "disaster_victim",
                schema: "kardesaile");
        }
    }
}
