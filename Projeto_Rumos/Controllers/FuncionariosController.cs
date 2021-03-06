using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Models_Class.Enum;
using Newtonsoft.Json;
using Projeto_Rumos.ApiConector;
using Projeto_Rumos.Models;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos.Controllers
{
    public class FuncionariosController : Controller
    {
        private readonly ContextApplication _context;
        private readonly ApiConnector _apiConnector;

        public FuncionariosController(ContextApplication context, ApiConnector apiConnector)
        {
            _context = context;
            _apiConnector = apiConnector;
        }

        // GET: Funcionarios
        //MOSTRA A LISTA DE USUARIOS NA BASE DE DADOS

        public IActionResult Index()
        {
            var response = _apiConnector.Get("Funcionarios");
            var result = JsonConvert.DeserializeObject<List<Funcionario>>(response);
            return View(result.ToList());
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
                var response = _apiConnector.Get("Funcionarios");
                var result = JsonConvert.DeserializeObject<List<Funcionario>>(response);
                //METODO DECLARADO A BAIXO
                var resultPass = ComputeHash(password, new SHA256CryptoServiceProvider());

                var Funcionario = result.FirstOrDefault(func => func.Email == email && func.Password == resultPass);
                if (Funcionario == null)
                {
                    ViewBag.message = "Ups !! Dados incorrectos!!!";
                    return View();
                }
                else if (Funcionario.Nome == nome && Funcionario.Email == email && Funcionario.Password == resultPass)
                {
                    return RedirectToAction("GestaoProduto","Produtos");
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
        public IActionResult Details(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var funcionario = _apiConnector.GetById("Funcionarios", id);
            var result = JsonConvert.DeserializeObject<Funcionario>(funcionario);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // GET: Funcionarios/Create
        public IActionResult CreateFuncionario()
        {
            List<Enum> listaDeCargos = new List<Enum>();

            listaDeCargos.Add(EnumCargo.Administrador);
            listaDeCargos.Add(EnumCargo.Empregado);
            listaDeCargos.Add(EnumCargo.Primeiro_Encarregado);
            listaDeCargos.Add(EnumCargo.Segundo_Encarregado);

            ViewBag.ListaDeCargos = listaDeCargos;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFuncionario([Bind("Id,Nome,Email,Password,NumeroDeTrabalhador,Cargo")] Funcionario funcionario)
        {
            if (ModelState.IsValid)
            {
                switch (funcionario.Cargo)
                {
                    case "Administrador":
                        funcionario.Cargo = EnumCargo.Administrador.ToString();
                    break;
                    case "Empregado":
                        funcionario.Cargo = EnumCargo.Empregado.ToString();
                        break;
                    case "Primeiro_Encarregado":
                        funcionario.Cargo = EnumCargo.Primeiro_Encarregado.ToString();
                        break;
                    case "Segundo_Encarregado":
                        funcionario.Cargo = EnumCargo.Segundo_Encarregado.ToString();
                        break;

                }
                var senha = funcionario.Password;
                var senhaEncriptada = ComputeHash(senha, new SHA256CryptoServiceProvider());
                funcionario.Password = senhaEncriptada;
                var funcionarioJson = JsonConvert.SerializeObject(funcionario);
                _apiConnector.Post("Funcionarios", funcionarioJson);
                return RedirectToAction(nameof(Index));
            }
            return View(funcionario);
        }

        // GET: Funcionarios/Edit/5
        public IActionResult Edit(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var funcionario = _apiConnector.GetById("Funcionarios", id);
            var result = JsonConvert.DeserializeObject<Funcionario>(funcionario);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,Nome,Email,Password,NumeroDeTrabalhador,Cargo")] Funcionario funcionario)
        {
            if (id != funcionario.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var senha = funcionario.Password;
                    var senhaEncriptada = ComputeHash(senha, new SHA256CryptoServiceProvider());
                    funcionario.Password = senhaEncriptada;
                    _apiConnector.Update("Funcionarios", funcionario.ToString());
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
        public IActionResult Delete(int id)
        {
            if (id.Equals(null))
            {
                return NotFound();
            }

            var funcionario = _apiConnector.GetById("Funcionarios", id);
            var result = JsonConvert.DeserializeObject<Funcionario>(funcionario);
            if (result == null)
            {
                return NotFound();
            }
            return View(result);
        }

        // POST: Funcionarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            _apiConnector.Delete("Funcionarios", id);
            return RedirectToAction(nameof(Index));
        }

        private bool FuncionarioExists(int id)
        {
            return bool.Parse(_apiConnector.GetById("Funcionarios",id));
        }

        //METODO PARA CRIPTAR A SENHA
        private string ComputeHash(string input, SHA256CryptoServiceProvider algotithm)
        {
            byte[] inputBytes = Encoding.UTF8.GetBytes(input);
            byte[] hashBytes = algotithm.ComputeHash(inputBytes);
            return BitConverter.ToString(hashBytes);
        }
    }
}
