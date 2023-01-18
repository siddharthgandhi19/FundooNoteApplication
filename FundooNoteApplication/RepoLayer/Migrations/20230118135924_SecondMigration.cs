using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CollabTable",
                columns: table => new
                {
                    CollabId = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(nullable: true),
                    UserId = table.Column<long>(nullable: false),
                    NoteID = table.Column<long>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CollabTable", x => x.CollabId);
                    table.ForeignKey(
                        name: "FK_CollabTable_NoteTable_NoteID",
                        column: x => x.NoteID,
                        principalTable: "NoteTable",
                        principalColumn: "NoteID",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_CollabTable_UserTable_UserId",
                        column: x => x.UserId,
                        principalTable: "UserTable",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_NoteID",
                table: "CollabTable",
                column: "NoteID");

            migrationBuilder.CreateIndex(
                name: "IX_CollabTable_UserId",
                table: "CollabTable",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CollabTable");
        }
    }
}
