using Microsoft.EntityFrameworkCore.Migrations;

namespace DawnPan.Migrations
{
    public partial class _02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directories_Directories_ParentId",
                table: "Directories");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "Directories",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Directories",
                type: "longtext",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddForeignKey(
                name: "FK_Directories_Directories_ParentId",
                table: "Directories",
                column: "ParentId",
                principalTable: "Directories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Directories_Directories_ParentId",
                table: "Directories");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Directories");

            migrationBuilder.AlterColumn<long>(
                name: "ParentId",
                table: "Directories",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Directories_Directories_ParentId",
                table: "Directories",
                column: "ParentId",
                principalTable: "Directories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
