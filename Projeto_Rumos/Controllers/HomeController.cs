using Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Projeto_Rumos.Models;
using WebApplication2.Data;
using Microsoft.EntityFrameworkCore;

namespace Projeto_Rumos.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ApplicationDbContext _dbContext;
        [Obsolete]
        private IHostingEnvironment _environment;

        [Obsolete]
        public HomeController(ILogger<HomeController> logger, ApplicationDbContext dbContext, IHostingEnvironment environment)
        {
            _logger = logger;
            _dbContext = dbContext;
            _environment = environment;
        }

        public IActionResult Index()
        {
            try
            {
                return View();
            }
            catch (Exception msg) { ErrorViewModel errorViewModel = new ErrorViewModel(); errorViewModel.RequestId = msg.Message; return View("_Error", errorViewModel); }
        }

        //RETORNA A VIEW COM OS ARTIGOS DA BASE DE DADOS
        public async Task<IActionResult> Produto()
        {
            try
            {
                return View(await _dbContext.Produtos.ToListAsync());
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

        //ACTION PARA IR BUSCAR A IMAGEM E ASSOCIAR AO PRODUTO
        //NÃO RETORNA VIEW A NÃO SER QUE DE ERRO
        [Obsolete]
        public IActionResult GetImage(int produtoId)
        {
            try
            {
                Produto requestedPhoto = _dbContext.Produtos.FirstOrDefault(p => p.ProdutoId == produtoId);
                if (requestedPhoto != null)
                {
                    string webRootpath = _environment.WebRootPath;
                    string folderPath = "\\img\\images_produtos\\";
                    string fullPath = webRootpath + folderPath + requestedPhoto.PhotoFileName;

                    FileStream fileOnDisk = new FileStream(fullPath, FileMode.Open);
                    byte[] fileBytes;
                    using (BinaryReader br = new BinaryReader(fileOnDisk))
                    {
                        fileBytes = br.ReadBytes((int)fileOnDisk.Length);
                    }
                    return File(fileBytes, requestedPhoto.ImageMimeType);
                }
                else
                {
                    return NotFound();
                }
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

        // ACTION PARA MOSTRAR O DETALHE DO ARTIGO PROCURA PELA "LUPA"
        public async Task<IActionResult> SearchDetails(int? id)
        {
            try
            {
                Produto produto = new Produto();
                if (id == null)
                {
                    return NotFound();
                }

                produto = await _dbContext.Produtos
                    .FirstOrDefaultAsync(m => m.ProdutoId == id);

                return View(produto);
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
                var resultados = _dbContext.Produtos
                        .FirstOrDefault(m => m.Nome == consulta);

                return View("SearchDetails", resultados);
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
