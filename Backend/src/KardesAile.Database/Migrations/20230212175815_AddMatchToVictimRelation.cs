using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KardesAile.Database.Migrations
{
    /// <inheritdoc />
    public partial class AddMatchToVictimRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match",
                column: "victim_id",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "fk_match_disaster_victim_victim_id",
                schema: "kardesaile",
                table: "match",
                column: "victim_id",
                principalSchema: "kardesaile",
                principalTable: "disaster_victim",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "fk_match_disaster_victim_victim_id",
                schema: "kardesaile",
                table: "match");

            migrationBuilder.DropIndex(
                name: "ix_match_victim_id",
                schema: "kardesaile",
                table: "match");
        }
    }
}
