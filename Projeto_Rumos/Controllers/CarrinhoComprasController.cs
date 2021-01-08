using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication2.Data;

namespace Projeto_Rumos.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private ApplicationDbContext _dbContext;

        public CarrinhoComprasController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }

        [HttpPost]
        public IActionResult Create(int id)
        {
            try
            {
                var prod = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                CarrinhoCompra carrinhoCompra = new CarrinhoCompra { Produto = prod };
                _dbContext.CarrinhoCompras.Add(carrinhoCompra);
                _dbContext.SaveChanges();

                return RedirectToAction(nameof(Index));
            } catch (Exception)
            {
                return RedirectToAction("Index");
            }   
        }

        public IActionResult Details(int id)
        {
            var produtoCarrinho = _dbContext.Produtos.FirstOrDefault(m => m.ProdutoId == id);

            return View(produtoCarrinho);
        }

        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).FirstOrDefaultAsync(m => m.Produto.ProdutoId == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).FirstOrDefaultAsync(p => p.IdProduto == id);
            _dbContext.CarrinhoCompras.Remove(produto);
            await _dbContext.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Index()
        {
            return View(await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).ToListAsync());
        }
    }
}
