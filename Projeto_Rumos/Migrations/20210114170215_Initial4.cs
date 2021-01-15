using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Rumos.Migrations
{
    public partial class Initial4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompras_Produtos_IdProduto",
                table: "CarrinhoCompras");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras");

            migrationBuilder.RenameColumn(
                name: "IdProduto",
                table: "CarrinhoCompras",
                newName: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_ProdutoId",
                table: "CarrinhoCompras",
                column: "ProdutoId");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompras_Produtos_ProdutoId",
                table: "CarrinhoCompras",
                column: "ProdutoId",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CarrinhoCompras_Produtos_ProdutoId",
                table: "CarrinhoCompras");

            migrationBuilder.DropIndex(
                name: "IX_CarrinhoCompras_ProdutoId",
                table: "CarrinhoCompras");

            migrationBuilder.RenameColumn(
                name: "ProdutoId",
                table: "CarrinhoCompras",
                newName: "IdProduto");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras",
                column: "IdProduto");

            migrationBuilder.AddForeignKey(
                name: "FK_CarrinhoCompras_Produtos_IdProduto",
                table: "CarrinhoCompras",
                column: "IdProduto",
                principalTable: "Produtos",
                principalColumn: "ProdutoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
