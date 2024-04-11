using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GREVocabulary.Business.Migrations
{
    /// <inheritdoc />
    public partial class AddTotalColumnToSpacedRepetitionSession : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Total",
                table: "SpacedRepetitionSessions",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "SpacedRepetitionSessions");
        }
    }
}
