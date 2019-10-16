using Microsoft.EntityFrameworkCore.Migrations;

namespace Tuto.Domain.Migrations
{
    public partial class addicollectionpropertiesforteacheinfoandregion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TeacherInfos_Subjects_SubjectId",
                table: "TeacherInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_Users_TeacherId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_TeacherInfos_SubjectId",
                table: "TeacherInfos");

            migrationBuilder.RenameColumn(
                name: "TeacherId",
                table: "TeacherSubjects",
                newName: "TeacherInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Cities_RegionId",
                table: "Cities",
                column: "RegionId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "Cities",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_TeacherInfos_TeacherInfoId",
                table: "TeacherSubjects",
                column: "TeacherInfoId",
                principalTable: "TeacherInfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cities_Regions_RegionId",
                table: "Cities");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherSubjects_TeacherInfos_TeacherInfoId",
                table: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Cities_RegionId",
                table: "Cities");

            migrationBuilder.RenameColumn(
                name: "TeacherInfoId",
                table: "TeacherSubjects",
                newName: "TeacherId");

            migrationBuilder.RenameIndex(
                name: "IX_TeacherSubjects_TeacherInfoId",
                table: "TeacherSubjects",
                newName: "IX_TeacherSubjects_TeacherId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherInfos_SubjectId",
                table: "TeacherInfos",
                column: "SubjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherInfos_Subjects_SubjectId",
                table: "TeacherInfos",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherSubjects_Users_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
