using System;
using Microsoft.EntityFrameworkCore.Migrations;
using NetTopologySuite.Geometries;

namespace Tuto.Domain.Migrations
{
    public partial class Removelocationfromcityaddcreationtimetoreview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Location",
                table: "Cities");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreationTime",
                table: "Reviews",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreationTime",
                table: "Reviews");

            migrationBuilder.AddColumn<Point>(
                name: "Location",
                table: "Cities",
                nullable: true);
        }
    }
}
