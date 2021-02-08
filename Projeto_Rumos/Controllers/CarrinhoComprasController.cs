using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models_Class;
using Projeto_Rumos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using WebApplication2.Data;
using Microsoft.AspNetCore.Http;
using Azure.Storage.Blobs;
using Azure.Core;
using Microsoft.Azure.Cosmos.Table;
using Models;

namespace Projeto_Rumos.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private ApplicationDbContext _dbContext;
        private readonly AuthenticatedUser _user;
        private readonly DadosStorage _dados;

        public CarrinhoComprasController(ApplicationDbContext dbContext, AuthenticatedUser user, DadosStorage dados)
        {
            _dbContext = dbContext;
            _user = user;
            _dados = dados;
        }
        //ACTION CASO APANHEM ALGUMA EXCEÇÃO ESTÃO A RETORNA A VIEW "_ERROR"
        // ACTION PARA CRIAR UM PRODUTO NO CARRINHO DE COMPRAS E ASSOCIAR UM ID DE LOGIN E UM ID DE CARRINHO
        // AQUI FAZ TAMBÉM A GESTÁO DE STOCK, CASO SEJA ADICIONADO UM PRODUTO AO CARRINHO É RETIRADO SO STOCK DESSE PRODUTO.
        // CASO SEJA REPOSTO, O STOCK TAMBÉM É REPOSTO.
        // RETORNA A MENSAGEM JASON PARA ACTIVAR O POPUP DA VISTA PARTIAL "_PopupPartialView.cshtml"

        [HttpPost]
        public IActionResult Create([FromBody] int[] dados)
        {
            try
            {
                var id = dados[0];
                var qta = dados[1];

                var user = _user;
                if (user.UserName != null)
                {
                    var prod = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                    
                    //metodo da class "dadosStorage"
                    _dados.InserirDados(prod, user, qta);
                    
                    //ESTE CODIGO É PARA REMOVER UNIDADE DO STOCK
                    ////--------------------------------------------
                    var NovoStockProduto = prod.Stock - 1;
                    Produto produtoActualizado = prod;
                    produtoActualizado.Stock = NovoStockProduto;

                    _dbContext.Update(produtoActualizado);
                    _dbContext.SaveChanges();
                    ////--------------------------------------------///
                    var sucesso = new { Sucesso = true, };
                    return Json(sucesso);
                }
                else
                {
                    var necLogin = new { necLogin = true };
                    return Json(necLogin);
                }
                ///ALTERNATIVA PARA GRAVAR NA BASE DE DADOS
                //carrinhoCompra.Produto = prod;
                ////carrinhoCompra.UsuarioId = Guid.Parse(user.Id);

                //_dbContext.Add(carrinhoCompra);
                //_dbContext.SaveChanges();
            }
            catch (Exception msg)
            {
                var necLogin = new { necLogin = msg.Message };
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
                var produto = await _dbContext.Produtos.FirstOrDefaultAsync(prod => prod.ProdutoId == id);
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
                var user = _user;
                var prod = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == id);
                await _dados.ApagarProdutoDaListaAsync(prod, user);
                //var produto = await _dbContext.CarrinhoCompras.Include(carrinho => carrinho.Produto).FirstOrDefaultAsync(p => p.ProdutoId == id);
                //_dbContext.CarrinhoCompras.Remove(produto);

                //ESTE CODIGO SERVE PARA REPOR O STOCK
                //-------------------------------------------------------------------
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

        public IActionResult Carrinho()
        {
            try
            {
                var userId = _dbContext.Users.ToList();
                if (_user.UserName != null)
                {
                    List<ListaDeProdCliente> listaDeProd = _dados.ListaDeCliente(_user);

                    var total = 0.0;
                    foreach (var prod in listaDeProd)
                    {
                        var totalProd = prod.Preco * prod.Quantidade;
                        total += totalProd;
                    }

                    ViewBag.total = total.ToString("F2");
                    return View(listaDeProd);
                }
                else
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = "Erro, necessário efetuar login para vermos a tua lista de compras. Obrigado";
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

