using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Tuto.Domain.Migrations
{
    public partial class Addsubjectcityregionupdateteacherinfomodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "TeacherInfos");

            migrationBuilder.AddColumn<Guid>(
                name: "CityId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "RegionId",
                table: "Users",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<int>(
                name: "MinimumWage",
                table: "TeacherInfos",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "TeacherInfos",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "SubjectId",
                table: "Lessons",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(nullable: true),
                    Location = table.Column<Point>(nullable: true),
                    RegionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Regions",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Regions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TeacherSubjects",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false, defaultValueSql: "NEWID()"),
                    TeacherId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TeacherSubjects", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TeacherSubjects_Users_TeacherId",
                        column: x => x.TeacherId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_CityId",
                table: "Users",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RegionId",
                table: "Users",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherInfos_SubjectId",
                table: "TeacherInfos",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_TeacherSubjects_TeacherId",
                table: "TeacherSubjects",
                column: "TeacherId");

            migrationBuilder.AddForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TeacherInfos_Subjects_SubjectId",
                table: "TeacherInfos",
                column: "SubjectId",
                principalTable: "Subjects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users",
                column: "CityId",
                principalTable: "Cities",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Regions_RegionId",
                table: "Users",
                column: "RegionId",
                principalTable: "Regions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Lessons_Subjects_SubjectId",
                table: "Lessons");

            migrationBuilder.DropForeignKey(
                name: "FK_TeacherInfos_Subjects_SubjectId",
                table: "TeacherInfos");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Cities_CityId",
                table: "Users");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Regions_RegionId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Cities");

            migrationBuilder.DropTable(
                name: "Regions");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "TeacherSubjects");

            migrationBuilder.DropIndex(
                name: "IX_Users_CityId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_RegionId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_TeacherInfos_SubjectId",
                table: "TeacherInfos");

            migrationBuilder.DropIndex(
                name: "IX_Lessons_SubjectId",
                table: "Lessons");

            migrationBuilder.DropColumn(
                name: "CityId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "RegionId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "MinimumWage",
                table: "TeacherInfos");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "TeacherInfos");

            migrationBuilder.DropColumn(
                name: "SubjectId",
                table: "Lessons");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "TeacherInfos",
                nullable: true);
        }
    }
}
