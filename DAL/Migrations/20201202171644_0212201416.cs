using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class _0212201416 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivoApoio_Aulas_AulaId",
                table: "ArquivoApoio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArquivoApoio",
                table: "ArquivoApoio");

            migrationBuilder.RenameTable(
                name: "ArquivoApoio",
                newName: "ArquivosApoio");

            migrationBuilder.RenameIndex(
                name: "IX_ArquivoApoio_AulaId",
                table: "ArquivosApoio",
                newName: "IX_ArquivosApoio_AulaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArquivosApoio",
                table: "ArquivosApoio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivosApoio_Aulas_AulaId",
                table: "ArquivosApoio",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ArquivosApoio_Aulas_AulaId",
                table: "ArquivosApoio");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ArquivosApoio",
                table: "ArquivosApoio");

            migrationBuilder.RenameTable(
                name: "ArquivosApoio",
                newName: "ArquivoApoio");

            migrationBuilder.RenameIndex(
                name: "IX_ArquivosApoio_AulaId",
                table: "ArquivoApoio",
                newName: "IX_ArquivoApoio_AulaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ArquivoApoio",
                table: "ArquivoApoio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ArquivoApoio_Aulas_AulaId",
                table: "ArquivoApoio",
                column: "AulaId",
                principalTable: "Aulas",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
