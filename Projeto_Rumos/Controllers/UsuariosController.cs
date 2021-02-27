using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;
using Projeto_Rumos.ApiConector;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos.Controllers
{
    [Authorize]
    public class UsuariosController : Controller
    {
        private readonly ApiConnector _apiConnector;

        public UsuariosController(ApiConnector apiConnector)
        {
            _apiConnector = apiConnector;
        }

        // GET: Usuarios
        public IActionResult Index()
        {
            var search = _apiConnector.Get("Usuarios");
            var result = JsonConvert.DeserializeObject<List<Usuario>>(search);
            return View(result.ToList());
        }

        // GET: Usuarios/Details/5
        public IActionResult Details(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var search = _apiConnector.GetById("Usuarios", id);
            var result = JsonConvert.DeserializeObject<Usuario>(search);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // GET: Usuarios/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("UsuarioId,Username,Password,Morada,DataNascimento,CartaoIdentificacao,Contacto,Email")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _apiConnector.Post("Usuarios", usuario.ToString());
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        // GET: Usuarios/Edit/5
        public IActionResult Edit(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }
            var search = _apiConnector.GetById("Usuarios", id);
            var result = JsonConvert.DeserializeObject<Usuario>(search);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Usuarios/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("UsuarioId,Username,Password,Morada,DataNascimento,CartaoIdentificacao,Contacto,Email")] Usuario usuario)
        {
            if (id != usuario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _apiConnector.Update("Usuarios", usuario.ToString());
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UsuarioExists(usuario.Id))
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
            return View(usuario);
        }

        // GET: Usuarios/Delete/5
        public IActionResult Delete(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }
            var search = _apiConnector.GetById("Usuarios", id);
            var result = JsonConvert.DeserializeObject<Usuario>(search);
            
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Usuarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _apiConnector.Delete("Usuarios", id);
            return RedirectToAction(nameof(Index));
        }

        private bool UsuarioExists(int id)
        {
            return bool.Parse(_apiConnector.GetById("Usuarios", id));
        }
    }
}
