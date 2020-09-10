using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class _0909201717 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MercadoPago_IpnId",
                table: "MercadoPago_Ipns",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MercadoPago_Ipns",
                table: "MercadoPago_Ipns",
                column: "MercadoPago_IpnId");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nome", "Preco" },
                values: new object[] { "FUNDAMENTAL", 162.67m });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nome", "Preco" },
                values: new object[] { "EDUCAÇÃO FINANCEIRA", 84.67m });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Descricao", "Nome", "Preco" },
                values: new object[] { 3, null, "INVESTIMENTOS", 172.67m });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_MercadoPago_Ipns",
                table: "MercadoPago_Ipns");

            migrationBuilder.DeleteData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "MercadoPago_IpnId",
                table: "MercadoPago_Ipns");

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Nome", "Preco" },
                values: new object[] { "Curso 1", 0m });

            migrationBuilder.UpdateData(
                table: "Cursos",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Nome", "Preco" },
                values: new object[] { "Curso 2", 0m });
        }
    }
}
