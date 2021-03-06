using Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Projeto_Rumos.Models;
using Models_Class;
using Projeto_Rumos.ApiConector;
using Newtonsoft.Json;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ContextApplication _dbContext;
        [Obsolete]
        private IWebHostEnvironment _environment;
        private readonly ApiConnector _apiConnector;

        public object JsonConver { get; private set; }

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, ContextApplication dbContext, IWebHostEnvironment environment, ApiConnector apiConnector)
        {
            _logger = logger;
            _dbContext = dbContext;
            _environment = environment;
            _apiConnector = apiConnector;
        }

        public IActionResult Index()
        {
            try
            {
                var search = _apiConnector.Get("Produtos");
                var result = JsonConvert.DeserializeObject<List<Produto>>(search);

                List<Produto> list = result.Where(prod => prod.Preco == 0.99f).ToList();
                return View(list);
            }
            catch (Exception msg) { ErrorViewModel errorViewModel = new ErrorViewModel(); errorViewModel.RequestId = msg.Message; return View("_Error", errorViewModel); }
        }

        //RETORNA A VIEW COM OS ARTIGOS DA BASE DE DADOS
        public IActionResult Produto()
        {
            try
            {
                ApiConnector apiConector = new ApiConnector();
                var result = apiConector.Get("Produtos");
                List<Produto> produtos = new List<Produto>();
                produtos = JsonConvert.DeserializeObject<List<Produto>>(result);
                return View(produtos.ToList());
            }
            catch
            {
                ErrorViewModel errorViewModel = new ErrorViewModel
                {
                    RequestId = "Mensagem de erro"
                };
                return View("_Error", errorViewModel);
            }
        }

        //RETORNA A VIEW SOBRE
        public IActionResult Sobre()
        {
            try
            {
                return View();
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        // RETORNA A VIEW CONTACTO
        public IActionResult Contacto()
        {
            try
            {
                return View();
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        //POST: HOME/CONTACTO
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contacto([Bind("ContactoId,Nome,Email,ContactoTelefonico,Mensagem")] Contacto contacto)
        {
            if (ModelState.IsValid)
            {
                _dbContext.Add(contacto);
                await _dbContext.SaveChangesAsync();
                //return RedirectToAction(nameof(Index));
            }
            return View(contacto);
        }

        // ACTION PARA MOSTRAR O DETALHE DO ARTIGO PROCURA PELA "LUPA"
        public IActionResult SearchDetails(string consulta)
        {
            try
            {
                if (consulta == null)
                {
                    return NotFound();
                }
                var search = _apiConnector.Get("Produtos");
                var listProdutos = JsonConvert.DeserializeObject<List<Produto>>(search);
                var resultado = listProdutos.SingleOrDefault(m => m.Nome.Trim() == consulta.Trim());
                return View(resultado);
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }

        // ACTION QUE RECEBE A STRING INTRODUZIDA NA LOPA E EFETUA A PESQUISA PARA ENVIAR PARA A ACTION ANTERIOR
        [HttpPost]
        public IActionResult Procurar(string consulta)
        {
            try
            {
                var search = _apiConnector.Get("Produtos");
                var listProdutos = JsonConvert.DeserializeObject<List<Produto>>(search);
                var resultado = listProdutos.FirstOrDefault(m => m.Nome.Trim() == consulta.Trim());
                int id = resultado.Id;
                return RedirectToAction("SearchDetails");
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
