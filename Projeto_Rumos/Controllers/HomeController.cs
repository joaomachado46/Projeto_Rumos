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

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
