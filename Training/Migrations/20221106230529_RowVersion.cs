using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Training.Migrations
{
    public partial class RowVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Cms",
                table: "Tags",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Cms",
                table: "Categories",
                type: "rowversion",
                rowVersion: true,
                nullable: true);

            migrationBuilder.AddColumn<byte[]>(
                name: "RowVersion",
                schema: "Cms",
                table: "Articles",
                type: "rowversion",
                rowVersion: true,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Cms",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Cms",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "RowVersion",
                schema: "Cms",
                table: "Articles");
        }
    }
}
