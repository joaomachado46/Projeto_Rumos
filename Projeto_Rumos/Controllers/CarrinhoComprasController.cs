using Microsoft.AspNetCore.Mvc;
using Models_Class;
using Projeto_Rumos.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Models;
using Projeto_Rumos.ApiConector;
using Newtonsoft.Json;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos.Controllers
{
    public class CarrinhoComprasController : Controller
    {
        private ContextApplication _dbContext;
        private readonly AuthenticatedUser _user;
        private readonly DadosStorage _dados;
        //CLASS PARA FAZER A LIGAÇÃO A API(COMTEM OS METODOS GET, PUT, POST, DELETE)
        private readonly ApiConnector _apiConnector;

        public CarrinhoComprasController(ContextApplication dbContext, AuthenticatedUser user, DadosStorage dados, ApiConnector apiConnector)
        {
            _dbContext = dbContext;
            _user = user;
            _dados = dados;
            _apiConnector = apiConnector;
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
                if (dados == null)
                {
                    var notData = new { notData = true };
                    return Json(notData);
                }

                var id = dados[0];
                var qta = dados[1];

                var user = _user;
                if (user.UserName != null)
                {
                    var prod = _apiConnector.GetById("Produtos", id);
                    var Produto = JsonConvert.DeserializeObject<Produto>(prod);
                    //metodo da class "dadosStorage"(ESTA CLASS FAZ A LIGAÇÃO AO AZURE STORAGE)
                    _dados.InserirDados(Produto, user, qta);

                    //ESTE CODIGO É PARA REMOVER UNIDADE DO STOCK(FAZ A LIGAÇÃO A API)
                    ////--------------------------------------------
                    var NovoStockProduto = Produto.Stock - qta;
                    Produto produtoActualizado = Produto;
                    produtoActualizado.Stock = NovoStockProduto;

                    var productSerialize = JsonConvert.SerializeObject(produtoActualizado);
                    var result = _apiConnector.Update("Produtos", productSerialize);
                   
                    ////--------------------------------------------///
                    if (result != null)
                    {
                        var sucesso = new { Sucesso = true, };
                        return Json(sucesso);
                    }
                    else
                    {
                        var necLogin = new { necLogin = true };
                        return Json(necLogin);
                    }
                }
                else
                {
                    var necessarioLogin = new { necessarioLogin = true };
                    return Json(necessarioLogin);
                }
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }
        // ACTION PARA CONSULTAR O DETALHE DOS ARTIGOS ADICIONADOS AO CARRINHO DE COMPRAS
        public IActionResult Details(int id)
        {
            try
            {
                var produtoCarrinho = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(produtoCarrinho);
                return View(result);
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }
        // ACTION PARA CONFIRMAR O DELETE DE UM ARTIGO DO CARRINHO DE COMPRAS
        public IActionResult Delete(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    return NotFound();
                }
                var produtoCarrinho = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(produtoCarrinho);
                if (result == null)
                {
                    return NotFound();
                }
                return View(result);
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
            var quantidade = 0;
            try
            {
                var user = _user;
                var produtoCarrinho = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(produtoCarrinho);
                var listForTakeQtaProdRemove = _dados.ListaDeCliente(user);
                await _dados.ApagarProdutoDaListaAsync(result, user);

                foreach (var item in listForTakeQtaProdRemove)
                {
                    if(item.Nome == result.Nome)
                    {
                        quantidade = item.Quantidade;
                    }
                }
                //ESTE CODIGO SERVE PARA REPOR O STOCK
                //-------------------------------------------------------------------
                var NovoStockProduto = result.Stock + quantidade;
                Produto produtoActualizado = result;
                produtoActualizado.Stock = NovoStockProduto;

                var productSerialize = JsonConvert.SerializeObject(produtoActualizado);
                var response = _apiConnector.Update("Produtos", productSerialize);
                if (response == null)
                {
                    ErrorViewModel errorViewModel = new ErrorViewModel();
                    errorViewModel.RequestId = "Erro ao actualizar produto";
                    return View("_Error", errorViewModel);
                }
                //--------------------------------------------------------------------
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

