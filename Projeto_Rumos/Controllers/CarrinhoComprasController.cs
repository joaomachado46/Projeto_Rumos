using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Projeto_Rumos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Data;
using Microsoft.AspNetCore.Http;

namespace Projeto_Rumos.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly AuthenticatedUser _user;
        readonly CarrinhoCompra carrinhoCompra = new CarrinhoCompra();

        public CarrinhoComprasController(ApplicationDbContext dbContext, AuthenticatedUser user)
        {
            _dbContext = dbContext;
            _user = user;
        }

        [HttpPost]
        public IActionResult Create([FromBody] int id)
        {
            if (User.Identity.IsAuthenticated)
            {
                var user = _user;
                try
                {
                    var prod = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                    var exist = _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).Where(x => x.ProdutoId == prod.ProdutoId && x.UsuarioId == Guid.Parse(user.Id)).Any();

                    if (exist == false)
                    {
                        carrinhoCompra.ProdutoId = prod.ProdutoId;
                        carrinhoCompra.UsuarioId = Guid.Parse(user.Id);

                        _dbContext.Add(carrinhoCompra);
                        _dbContext.SaveChanges();

                        var sucesso = new { Sucesso = true, };
                        return Json(sucesso);
                    }
                    else
                    {
                        var sucesso = new { Sucesso = false };
                        return Json(sucesso);
                    }
                }
                catch
                {
                    var necLogin = new { necLogin = "necessarioLogin" };
                    return Json(necLogin);
                }
            }
            else
            {
                var necLogin = new { necLogin = true };
                return Json(necLogin);
            }
        }

        public IActionResult Details(int id)
        {
            try
            {
                var produtoCarrinho = _dbContext.Produtos.FirstOrDefault(m => m.ProdutoId == id);

                return View(produtoCarrinho);
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Delete(int? id)
        {
            try
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
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var produto = await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).FirstOrDefaultAsync(p => p.ProdutoId == id);
                _dbContext.CarrinhoCompras.Remove(produto);
                await _dbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Carrinho));
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Carrinho()
        {
            try
            {
                if (User.Identity.IsAuthenticated)
                {
                    var user = _user;
                    return View(await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).Where(x => x.UsuarioId == Guid.Parse(user.Id)).ToListAsync());
                }
                else
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = "Necessário Login";

                    return View("_Error", errorViewModel);
                }

            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }
    }
}
