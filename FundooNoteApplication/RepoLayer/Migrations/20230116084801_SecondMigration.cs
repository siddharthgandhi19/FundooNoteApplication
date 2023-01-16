using Microsoft.EntityFrameworkCore.Migrations;

namespace RepoLayer.Migrations
{
    public partial class SecondMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "UserId",
                table: "NoteTable",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateIndex(
                name: "IX_NoteTable_UserId",
                table: "NoteTable",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NoteTable_UserTable_UserId",
                table: "NoteTable",
                column: "UserId",
                principalTable: "UserTable",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NoteTable_UserTable_UserId",
                table: "NoteTable");

            migrationBuilder.DropIndex(
                name: "IX_NoteTable_UserId",
                table: "NoteTable");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "NoteTable");
        }
    }
}
