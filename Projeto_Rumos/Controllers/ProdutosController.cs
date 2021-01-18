using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using WebApplication2.Data;
using System.Globalization;
using Microsoft.AspNetCore.Authorization;
using Projeto_Rumos.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace Projeto_Rumos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _WebHost;

        public ProdutosController(ApplicationDbContext context, IWebHostEnvironment webHost)
        {
            _context = context;
            _WebHost = webHost;
        }

        // GET: Produtos
        
        [HttpPost]
        public async Task<IActionResult> SalvarImg(IFormFile ifile)
        {
            try
            {
                string imgext = Path.GetExtension(ifile.FileName);
                if (imgext == ".jpg" || imgext == ".gif" || imgext == ".png" || imgext == ".jpeg")
                {
                    var saveimg = Path.Combine(_WebHost.WebRootPath, "img/images_produtos", ifile.FileName);
                    var stream = new FileStream(saveimg, FileMode.Create);
                    await ifile.CopyToAsync(stream);
                    string nomeProduto = ifile.FileName;
                    ViewBag.Message = "Imagem carregada: " + nomeProduto;
                    ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Nome", "Nome");
                    return View("Create");
                }
                else
                {
                    ViewBag.Message = "Erro!! Carregue uma imagem válida";
                    ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Nome", "Nome");
                    return View("Create");
                }
            }
            catch (Exception msg)
            {

                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        // GET: Produtos/Details/5
        [HttpPost]
        public IActionResult Procurar(string consulta)
        {
            try
            {
                var resultados = _context.Produtos
                        .FirstOrDefault(m => m.Nome == consulta);

                return View("Details", resultados);
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        public async Task<IActionResult> Details(int? id)
        {
            try
            {
                Produto produto = new Produto();
                if (id == null)
                {
                    return NotFound();
                }

                produto = await _context.Produtos
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

        // GET: Produtos/Create
        public IActionResult CreateProduto()
        {
            try
            {
                ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Nome", "Nome");
                return View();
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string nome, string preco, string descricao, string photoFileName, int stock, Categoria categoria)
        {
            try
            {
                var precoCorreto = float.Parse(preco, CultureInfo.InvariantCulture.NumberFormat);

                var produto = new Produto { Nome = nome, Preco = precoCorreto, Descricao = descricao, PhotoFileName = photoFileName, ImageMimeType = "image/jpeg", Stock = stock, IdCategoria = categoria.CategoriaId };

                if (ModelState.IsValid)
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();

                    ViewBag.Message2 = "Produto adicionado com sucesso";
                    return View();
                }

                
                return View();
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        // GET: Produtos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var produto = await _context.Produtos.FindAsync(id);
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

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ProdutoId,Nome,Preco,Descricao,PhotoFileName,ImageMimeType,Stock")] Produto produto)
        {
            try
            {
                if (id != produto.ProdutoId)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.Update(produto);
                        await _context.SaveChangesAsync();
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.ProdutoId))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(Produto));
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

        // GET: Produtos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return NotFound();
                }

                var produto = await _context.Produtos
                    .FirstOrDefaultAsync(m => m.ProdutoId == id);
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

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                var produto = await _context.Produtos.FindAsync(id);
                _context.Produtos.Remove(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Produto));
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }
        public IActionResult MenuGestaoProduto()
        {
            return View();
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
