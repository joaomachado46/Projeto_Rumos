using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Rumos.Migrations
{
    public partial class updateLogin : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras",
                column: "IdProduto",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_IdProduto",
                table: "CarrinhoCompras",
                column: "IdProduto");
        }
    }
}
