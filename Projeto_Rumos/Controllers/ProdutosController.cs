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

namespace Projeto_Rumos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ProdutosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        public async Task<IActionResult> Produto()
        {
            try
            {
                return View(await _context.Produtos.ToListAsync());
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
        [Authorize]
        public IActionResult Create()
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

        // POST: Produtos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string nome, string preco, string descricao, string photoFileName, int stock)
        {
            try
            {
                var precoCorreto = float.Parse(preco, CultureInfo.InvariantCulture.NumberFormat);

                var produto = new Produto { Nome = nome, Preco = precoCorreto, Descricao = descricao, PhotoFileName = photoFileName, ImageMimeType = "image/jpeg", Stock = stock };

                if (ModelState.IsValid)
                {
                    _context.Add(produto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
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

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.ProdutoId == id);
        }
    }
}
