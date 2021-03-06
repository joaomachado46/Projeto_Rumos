using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Models;
using Models_Class;
using Models_Class.Enum;
using Projeto_Rumos.Areas.Identity.Pages.Account.UserData;

namespace WebApiFrutaria.DataContext
{
    public class ContextApplication : IdentityDbContext<ApplicationUser>
    {
        public ContextApplication(DbContextOptions<ContextApplication> options) : base (options)
        {
        }

        public DbSet<CarrinhoCompra> CarrinhoCompras { get; set; }
        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Encomenda> Encomendas { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Pagamento> Pagamentos { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Contacto> Contactos { get; set; }

        public DbSet<ApplicationUser> ApplicationUsers { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //SEEDING DE PRODUTOS
            modelBuilder.Entity<Produto>().HasData(
                new Produto
                {
                    Id = 1,
                    Nome = "Banana",
                    Preco = 0.99f,
                    Descricao = "Banana importada da Colombia.",
                    PhotoFileName = "banana1.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 10,
              
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/banana1.jpg"

                },
                new Produto
                {
                    Id = 2,
                    Nome = "Clementina",
                    Preco = 0.79f,
                    Descricao = "Clementina natural de Portugal",
                    PhotoFileName = "clementina.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 100,

                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/clementina.jpg"
                },
                new Produto
                {
                    Id = 3,
                    Nome = "Maça Fuji",
                    Preco = 0.59f,
                    Descricao = "Maça fuji, importada",
                    PhotoFileName = "fuji.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 150,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/fuji.jpg"
                },
                new Produto
                {
                    Id = 4,
                    Nome = "Kiwi",
                    Preco = 3.99f,
                    Descricao = "Kiwi, directamente da nossa quinta",
                    PhotoFileName = "kiwi.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 300,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/kiwi.jpg"
                },
                new Produto
                {
                    Id = 5,
                    Nome = "Limão",
                    Preco = 0.99f,
                    Descricao = "O melhor limão de Portugal",
                    PhotoFileName = "limao-siciliano.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 150,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/limao-siciliano.jpg"
                },
                new Produto
                {
                    Id = 6,
                    Nome = "Melão",
                    Preco = 1.99f,
                    Descricao = "O melhor melão. Importado do Brasil",
                    PhotoFileName = "melao.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 200,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/melao.jpg"
                },
                new Produto
                {
                    Id = 7,
                    Nome = "Peras",
                    Preco = 0.99f,
                    Descricao = "Pera natural, diretamente da nossa quinta",
                    PhotoFileName = "peras.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 200,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/peras.jpg"
                },
                new Produto
                {
                    Id = 8,
                    Nome = "Uva",
                    Preco = 1.29f,
                    Descricao = "Uva do Alentejo",
                    PhotoFileName = "uvas.jpg",
                    ImageMimeType = "image/jpeg",
                    Stock = 300,
                    
                    Url = "https://ac2020storage.blob.core.windows.net/joaomachado/uvas.jpg"
                });

            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<EncomendaProduto>().HasKey(ep => new { ep.EncomendaId, ep.Id });


            //SEEDING DE CATEGORIAS
            modelBuilder.Entity<Categoria>().HasData(
                new Categoria { Id = 1, Nome = "Frutas" },
                new Categoria { Id = 2, Nome = "Legumes" });

            //SEEDING DE FUNCIONARIO PARA TESTES
            modelBuilder.Entity<Funcionario>().HasData(new Funcionario
            {
                Id = 1,
                Nome = "Admin",
                Email = "admin@gmail.com",
                Password = "12345",
                NumeroDeTrabalhador = 156,
                Cargo = EnumCargo.Administrador.ToString(),
            });

            //modelBuilder.Entity<ApplicationUser>().HasData(new ApplicationUser
            //{
            //    Email = "admim@gmail.com",
            //    UserName = "admim",
            //    SobreNome = "admim",
            //    StreetAddress = "avenida",
            //    DateOfBirth = DateTime.Now,
            //    PhoneNumber = "9185552522",
            //    PasswordHash = "Joao.12",
            //});
        }
    }
}

