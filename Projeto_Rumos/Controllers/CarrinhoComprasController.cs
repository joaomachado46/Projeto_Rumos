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
        //ACTION CASO APANHEM ALGUMA EXCEÇÃO ESTÃO A RETORNA A VIEW "_ERROR"
        // ACTION PARA CRIAR UM PRODUTO NO CARRINHO DE COMPRAS E ASSOCIAR UM ID DE LOGIN E UM ID DE CARRINHO
        // AQUI FAZ TAMBÉM A GESTÁO DE STOCK, CASO SEJA ADICIONADO UM PRODUTO AO CARRINHO É RETIRADO SO STOCK DESSE PRODUTO.
        // CASO SEJA REPOSTO, O STOCK TAMBÉM É REPOSTO.
        // RETORNA A MENSAGEM JASON PARA ACTIVAR O POPUP DA VISTA PARTIAL "_PopupPartialView.cshtml"
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

                    //ESTE CODIGO É PARA REMOVER UNIDADE DO STOCK
                    //--------------------------------------------
                    var NovoStockProduto = prod.Stock - 1;
                    Produto produtoActualizado = prod;
                    produtoActualizado.Stock = NovoStockProduto;

                    _dbContext.Update(produtoActualizado);
                    _dbContext.SaveChanges();
                    //--------------------------------------------

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

        // ACTION PARA CONSULTAR O DETALHE DOS ARTIGOS ADICIONADOS AO CARRINHO DE COMPRAS

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

        // ACTION PARA CONFIRMAR O DELETE DE UM ARTIGO DO CARRINHO DE COMPRAS

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

        // ACTION PARA FAZER O DELETE DE UM ARTIGO DO CARRINHO DE COMPRAS
        //AQUI TEM TAMBÉM O CODIGO PARA REPOR O STOCK CASO A VENDA NÃO SE FINALIZE
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var produto = await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).FirstOrDefaultAsync(p => p.ProdutoId == id);
                _dbContext.CarrinhoCompras.Remove(produto);

                //ESTE CODIGO SERVE PARA REPOR O STOCK
                //-------------------------------------------------------------------

                var prod = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                var NovoStockProduto = prod.Stock + 1;
                Produto produtoActualizado = prod;
                produtoActualizado.Stock = NovoStockProduto;

                _dbContext.Update(produtoActualizado);
                _dbContext.SaveChanges();

                //--------------------------------------------------------------------
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

        // ACTION PARA ABRIR O CARRINHO DE COMPRAS, ESTA VIEW SO ABRE SE TIVER LOGIN FEITO, POIS CADA USUARIO TEM ACESSO SOMENTE
        //AO SEU CARRINHO.

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

