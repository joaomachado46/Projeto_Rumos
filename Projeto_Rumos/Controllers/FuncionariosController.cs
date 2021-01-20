using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Projeto_Rumos.Models;
using WebApplication2.Data;

namespace Projeto_Rumos.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Funcionarios
        //MOSTRA A LISTA DE USUARIOS NA BASE DE DADOS

        public async Task<IActionResult> Index()
        {
            return View(await _context.Funcionarios.ToListAsync());
        }

        //ACTION PARA A VIEW DE LOGIN DE FUNCIONARIO.
        public IActionResult LoginFuncionario()
        {
            return View();
        }

        //ACTION PARA RETORNAR A VIR DE MENU GESTÃO
        public IActionResult MenuGestao()
        {
            return View();
        }

        //ACTION PARA LOGIN DE FUNCIONARO, FAZ CONFIRMAÇÃO POR NOME, EMAIL E A PASSWORD
        //SE ERRADO O LOGIN ENVIA PARA A VIEW UM VIEWBAG.MESSAGE, SE OK, FA UM REDIRECT PARA ACTION "MenuGestao"

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult LoginFuncionario(string nome, string email, string password)
        {
            try
            {
                var Funcionario = _context.Funcionarios.FirstOrDefault(func => func.Email == email && func.Password == password);
                if (Funcionario == null)
                {
                    ViewBag.message = "Ups !! Dados incorrectos!!!";
                    return View();
                }
                else if (Funcionario.Nome == nome && Funcionario.Email == email && Funcionario.Password == password)
                {
                    return RedirectToAction(nameof(MenuGestao));
                }
                else
                {
                    return View();
                }
            }
            catch (Exception msg)
            {

                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        // GET: Funcionarios/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // GET: Funcionarios/Create
        public IActionResult CreateFuncionario()
        {
            return View();
        }

        // POST: Funcionarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateFuncionario([Bind("Id,Nome,Email,Password,NumeroDeTrabalhador,Cargo")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(funcionario);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null)
            {
                return NotFound();
            }
            return View(funcionario);
        }

        // POST: Funcionarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Email,Password,NumeroDeTrabalhador,Cargo")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(funcionario);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FuncionarioExists(funcionario.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(m => m.Id == id);
            if (funcionario == null)
            {
                return NotFound();
            }

            return View(funcionario);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            _context.Funcionarios.Remove(funcionario);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return _context.Funcionarios.Any(e => e.Id == id);
        }
    }
}
