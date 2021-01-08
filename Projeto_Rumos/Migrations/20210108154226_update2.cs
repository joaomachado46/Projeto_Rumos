using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Rumos.Migrations
{
    public partial class update2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1,
                column: "Preco",
                value: 0.99m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 2,
                column: "Preco",
                value: 0.79m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 3,
                column: "Preco",
                value: 0.59m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 4,
                column: "Preco",
                value: 3.99m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 5,
                column: "Preco",
                value: 0.99m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 6,
                column: "Preco",
                value: 1.99m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 7,
                column: "Preco",
                value: 0.99m);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 8,
                column: "Preco",
                value: 1.29m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Preco",
                table: "Produtos",
                type: "float",
                nullable: false,
                oldClrType: typeof(decimal));

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1,
                column: "Preco",
                value: 0.98999999999999999);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 2,
                column: "Preco",
                value: 0.79000000000000004);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 3,
                column: "Preco",
                value: 0.58999999999999997);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 4,
                column: "Preco",
                value: 3.9900000000000002);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 5,
                column: "Preco",
                value: 0.98999999999999999);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 6,
                column: "Preco",
                value: 1.99);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 7,
                column: "Preco",
                value: 0.98999999999999999);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 8,
                column: "Preco",
                value: 1.29);
        }
    }
}
