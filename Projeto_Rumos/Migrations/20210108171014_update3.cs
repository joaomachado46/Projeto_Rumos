using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Rumos.Migrations
{
    public partial class update3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<float>(
                name: "Preco",
                table: "Produtos",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 1,
                column: "Preco",
                value: 0.99f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 2,
                column: "Preco",
                value: 0.79f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 3,
                column: "Preco",
                value: 0.59f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 4,
                column: "Preco",
                value: 3.99f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 5,
                column: "Preco",
                value: 0.99f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 6,
                column: "Preco",
                value: 1.99f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 7,
                column: "Preco",
                value: 0.99f);

            migrationBuilder.UpdateData(
                table: "Produtos",
                keyColumn: "ProdutoId",
                keyValue: 8,
                column: "Preco",
                value: 1.29f);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Preco",
                table: "Produtos",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(float));

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
    }
}
