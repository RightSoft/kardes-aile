using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class FixMatchWithManySupporterAndDisasterVictim : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_match_supporter_id",
                schema: "kardesaile",
                table: "match");

            migrationBuilder.DropIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match");

            migrationBuilder.CreateIndex(
                name: "ix_match_supporter_id",
                schema: "kardesaile",
                table: "match",
                column: "supporter_id");

            migrationBuilder.CreateIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match",
                column: "victim_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_match_supporter_id",
                schema: "kardesaile",
                table: "match");

            migrationBuilder.DropIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match");

            migrationBuilder.CreateIndex(
                name: "ix_match_supporter_id",
                schema: "kardesaile",
                table: "match",
                column: "supporter_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match",
                column: "victim_id",
                unique: true);
        }
    }
}
