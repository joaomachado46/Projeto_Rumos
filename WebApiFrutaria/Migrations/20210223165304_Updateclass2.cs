using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebApiFrutaria.Migrations
{
    public partial class Updateclass2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categorias",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categorias", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contactos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactoTelefonico = table.Column<int>(type: "int", nullable: false),
                    Mensagem = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contactos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Funcionarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    NumeroDeTrabalhador = table.Column<int>(type: "int", nullable: false),
                    Cargo = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Funcionarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Morada = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Preco = table.Column<float>(type: "real", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhotoFileName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageMimeType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    Url = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoriaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Produtos_Categorias_CategoriaId",
                        column: x => x.CategoriaId,
                        principalTable: "Categorias",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Encomendas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    Estado = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Encomendas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Encomendas_Usuarios_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CarrinhoCompras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrinhoCompras", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompras_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompras_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CarrinhoCompras_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EncomendaProduto",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
                    EncomendaId = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EncomendaProduto", x => new { x.EncomendaId, x.Id });
                    table.ForeignKey(
                        name: "FK_EncomendaProduto_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EncomendaProduto_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Pagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CarrinhoId = table.Column<int>(type: "int", nullable: true),
                    CarrihoComprasId = table.Column<int>(type: "int", nullable: true),
                    EncomendaId = table.Column<int>(type: "int", nullable: true),
                    UsuarioId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pagamentos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Pagamentos_CarrinhoCompras_CarrihoComprasId",
                        column: x => x.CarrihoComprasId,
                        principalTable: "CarrinhoCompras",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Encomendas_EncomendaId",
                        column: x => x.EncomendaId,
                        principalTable: "Encomendas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Pagamentos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Categorias",
                columns: new[] { "Id", "Nome" },
                values: new object[,]
                {
                    { 1, "Frutas" },
                    { 2, "Legumes" }
                });

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Cargo", "Email", "Nome", "NumeroDeTrabalhador", "Password" },
                values: new object[] { 1, 3, "admin@gmail.com", "Admin", 156, "12345" });

            migrationBuilder.InsertData(
                table: "Produtos",
                columns: new[] { "Id", "CategoriaId", "Descricao", "ImageMimeType", "Nome", "PhotoFileName", "Preco", "Stock", "Url" },
                values: new object[,]
                {
                    { 1, null, "Banana importada da Colombia.", "image/jpeg", "Banana", "banana1.jpg", 0.99f, 10, "https://ac2020storage.blob.core.windows.net/joaomachado/banana1.jpg" },
                    { 2, null, "Clementina natural de Portugal", "image/jpeg", "Clementina", "clementina.jpg", 0.79f, 100, "https://ac2020storage.blob.core.windows.net/joaomachado/clementina.jpg" },
                    { 3, null, "Maça fuji, importada", "image/jpeg", "Maça Fuji", "fuji.jpg", 0.59f, 150, "https://ac2020storage.blob.core.windows.net/joaomachado/fuji.jpg" },
                    { 4, null, "Kiwi, directamente da nossa quinta", "image/jpeg", "Kiwi", "kiwi.jpg", 3.99f, 300, "https://ac2020storage.blob.core.windows.net/joaomachado/kiwi.jpg" },
                    { 5, null, "O melhor limão de Portugal", "image/jpeg", "Limão", "limao-siciliano.jpg", 0.99f, 150, "https://ac2020storage.blob.core.windows.net/joaomachado/limao-siciliano.jpg" },
                    { 6, null, "O melhor melão. Importado do Brasil", "image/jpeg", "Melão", "melao.jpg", 1.99f, 200, "https://ac2020storage.blob.core.windows.net/joaomachado/melao.jpg" },
                    { 7, null, "Pera natural, diretamente da nossa quinta", "image/jpeg", "Peras", "peras.jpg", 0.99f, 200, "https://ac2020storage.blob.core.windows.net/joaomachado/peras.jpg" },
                    { 8, null, "Uva do Alentejo", "image/jpeg", "Uva", "uvas.jpg", 1.29f, 300, "https://ac2020storage.blob.core.windows.net/joaomachado/uvas.jpg" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_EncomendaId",
                table: "CarrinhoCompras",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_ProdutoId",
                table: "CarrinhoCompras",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_CarrinhoCompras_UsuarioId",
                table: "CarrinhoCompras",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_EncomendaProduto_ProdutoId",
                table: "EncomendaProduto",
                column: "ProdutoId");

            migrationBuilder.CreateIndex(
                name: "IX_Encomendas_IdUsuario",
                table: "Encomendas",
                column: "IdUsuario");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_CarrihoComprasId",
                table: "Pagamentos",
                column: "CarrihoComprasId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_EncomendaId",
                table: "Pagamentos",
                column: "EncomendaId");

            migrationBuilder.CreateIndex(
                name: "IX_Pagamentos_UsuarioId",
                table: "Pagamentos",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Produtos_CategoriaId",
                table: "Produtos",
                column: "CategoriaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contactos");

            migrationBuilder.DropTable(
                name: "EncomendaProduto");

            migrationBuilder.DropTable(
                name: "Funcionarios");

            migrationBuilder.DropTable(
                name: "Pagamentos");

            migrationBuilder.DropTable(
                name: "CarrinhoCompras");

            migrationBuilder.DropTable(
                name: "Encomendas");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Categorias");
        }
    }
}
