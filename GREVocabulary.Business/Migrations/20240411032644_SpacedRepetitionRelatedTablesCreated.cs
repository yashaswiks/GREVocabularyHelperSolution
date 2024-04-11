using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GREVocabulary.Business.Migrations
{
    /// <inheritdoc />
    public partial class SpacedRepetitionRelatedTablesCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SpacedRepetitionSessions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Red = table.Column<int>(type: "INTEGER", nullable: false),
                    Green = table.Column<int>(type: "INTEGER", nullable: false),
                    SessionTimestamp = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpacedRepetitionSessions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SessionDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    GroupId = table.Column<int>(type: "INTEGER", nullable: false),
                    WordToMemorize = table.Column<string>(type: "nvarchar(350)", nullable: true),
                    Red = table.Column<bool>(type: "INTEGER", nullable: false),
                    Green = table.Column<bool>(type: "INTEGER", nullable: false),
                    SpacedRepetitionSessionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SessionDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SessionDetails_SpacedRepetitionSessions_SpacedRepetitionSessionId",
                        column: x => x.SpacedRepetitionSessionId,
                        principalTable: "SpacedRepetitionSessions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SessionDetails_SpacedRepetitionSessionId",
                table: "SessionDetails",
                column: "SpacedRepetitionSessionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SessionDetails");

            migrationBuilder.DropTable(
                name: "SpacedRepetitionSessions");
        }
    }
}
