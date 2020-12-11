using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class _09110952 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "ModulosAlunos",
                type: "decimal(3, 1)",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Nota",
                table: "ModulosAlunos",
                type: "decimal(3, 1)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(3, 1)",
                oldNullable: true);
        }
    }
}
