using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuto.Domain.Migrations
{
    public partial class renameallprimarykeystoid : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TeacherInfoId",
                table: "TeacherInfos",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ReviewId",
                table: "Reviews",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "LessonId",
                table: "Lessons",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ChatMessageId",
                table: "ChatMessages",
                newName: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "TeacherInfos",
                newName: "TeacherInfoId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Reviews",
                newName: "ReviewId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Lessons",
                newName: "LessonId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ChatMessages",
                newName: "ChatMessageId");
        }
    }
}
