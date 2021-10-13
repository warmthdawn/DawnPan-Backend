using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DawnPan.Migrations
{
    public partial class _01 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Hash",
                table: "Files",
                type: "blob(384)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob(256)",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "Hash",
                table: "Files",
                type: "blob(256)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "blob(384)",
                oldNullable: true);
        }
    }
}
