using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddCountryCodeStateCodeColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_city_name",
                schema: "kardesaile",
                table: "city");

            migrationBuilder.AddColumn<string>(
                name: "country_code",
                schema: "kardesaile",
                table: "country",
                type: "character varying(3)",
                maxLength: 3,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "state_code",
                schema: "kardesaile",
                table: "city",
                type: "character varying(5)",
                maxLength: 5,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_city_name",
                schema: "kardesaile",
                table: "city",
                column: "name");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_city_name",
                schema: "kardesaile",
                table: "city");

            migrationBuilder.DropColumn(
                name: "country_code",
                schema: "kardesaile",
                table: "country");

            migrationBuilder.DropColumn(
                name: "state_code",
                schema: "kardesaile",
                table: "city");

            migrationBuilder.CreateIndex(
                name: "ix_city_name",
                schema: "kardesaile",
                table: "city",
                column: "name",
                unique: true);
        }
    }
}
