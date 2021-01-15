using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Projeto_Rumos.Migrations
{
    public partial class Initial3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pagamentos_Usuarios_UsuarioId",
                table: "Pagamentos");

            migrationBuilder.DropIndex(
                name: "IX_Pagamentos_UsuarioId",
                table: "Pagamentos");

            migrationBuilder.AddColumn<Guid>(
                name: "UsuarioId",
                table: "CarrinhoCompras",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsuarioId",
                table: "CarrinhoCompras");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_UsuarioId",
                table: "Pagamentos",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pagamentos_Usuarios_UsuarioId",
                table: "Pagamentos",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "UsuarioId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
