using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using System.Globalization;
using Projeto_Rumos.Models;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Projeto_Rumos.ApiConector;
using Newtonsoft.Json;
using WebApiFrutaria.DataContext;

namespace Projeto_Rumos.Controllers
{
    public class ProdutosController : Controller
    {
        private readonly ContextApplication _context;
        private readonly IWebHostEnvironment _WebHost;
        private readonly DadosStorage _dados;
        private readonly ApiConnector _apiConnector;

        public ProdutosController(ContextApplication context, IWebHostEnvironment webHost, DadosStorage dados, ApiConnector apiConnector)
        {
            _context = context;
            _WebHost = webHost;
            _dados = dados;
            _apiConnector = apiConnector;
        }
        // GET: Produtos
        // ACTION PARA SALVAR A IMAGEM QUE VAI SER ASSOCIADA AO PRODUTO, É RETURNADO O LINK DO AZURE STORAGE
        [HttpPost]
        public IActionResult SalvarImg(List<IFormFile> files)
        {
            try
            {
                var result = "";
                foreach (var file in files)
                {
                    if (file.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            var fileName = file.FileName;
                            file.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            string s = Convert.ToBase64String(fileBytes);
                            // act on the Base64 data
                            var resultConvert = JsonConvert.SerializeObject(s+fileName);
                            result = _apiConnector.SaveImageAzureBlob(resultConvert, "Produtos", "saveimage");
                        }
                    }
                }

                ViewBag.Url = result;
                ViewBag.Message = "Imagem(s) Carregada(s):";
                //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Nome", "Nome");
                return View("CreateProduto");   
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }

        // GET: Produtos/Details/5
        public IActionResult Details(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    return NotFound();
                }

                var search = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(search);

                return View(result);
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }

        // GET: Produtos/Create
        // retorna para a view um viewdata com as categorias, para ser preenchido um select com os nomes das categorias
        public IActionResult CreateProduto()
        {
            try
            {
                //ViewData["IdCategoria"] = new SelectList(_context.Categorias, "Nome", "Nome");
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
        // Metodo para criar um novo produto da área de funcionario, "prop: photoFileName", é introduzida automaticamente no controller
        // pois como é igual para todos os produtos, não é necessario escrever.
        // retorna uma viewbag.message consoante se der correto ou não e passa essa message para a view.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(string nome, string preco, string descricao, string photoFileName, int stock, string url)
        {
            try
            {
                var Preco = float.Parse(preco, CultureInfo.InvariantCulture.NumberFormat);
                var produto = new Produto { Nome = nome, Preco = Preco, Descricao = descricao, PhotoFileName = photoFileName, ImageMimeType = "image/jpeg", Stock = stock, Url=url};

                var data = JsonConvert.SerializeObject(produto);
                var result = _apiConnector.Post("Produtos", data);
                if (result == null)
                {
                    return null;
                }
                else
                {
                    ViewBag.Message2 = "Produto adicionado com sucesso";
                    return View("CreateProduto");
                }
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;
                return View("_Error", errorViewModel);
            }
        }

        // GET: Produtos/Edit/5
        public IActionResult Edit(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    return NotFound();
                }
                var search = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(search);
                
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

        // POST: Produtos/Edit/5
        // AACTION PARA EDITAR UM PRODUTO DA AREA DE GESTÁO DE PRODUTO

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, string Preco, [Bind("Id,Nome,Descricao,PhotoFileName,ImageMimeType,Stock")] Produto produto)
        {
            try
            {
                var preco = float.Parse(Preco, CultureInfo.InvariantCulture.NumberFormat);
                produto.Preco = preco;

                if (id != produto.Id)
                {
                    return NotFound();
                }

                if (ModelState.IsValid)
                {
                    try
                    {
                        _apiConnector.Update("Produtos", produto.ToString());
                    }
                    catch (DbUpdateConcurrencyException)
                    {
                        if (!ProdutoExists(produto.Id))
                        {
                            return NotFound();
                        }
                        else
                        {
                            throw;
                        }
                    }
                    return RedirectToAction(nameof(GestaoProduto));
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
        public IActionResult Delete(int id)
        {
            try
            {
                if (id.Equals(null))
                {
                    return NotFound();
                }
                var search = _apiConnector.GetById("Produtos", id);
                var result = JsonConvert.DeserializeObject<Produto>(search);
                
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

        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            try
            {
                _apiConnector.Delete("Produtos", id);
                return RedirectToAction(nameof(GestaoProduto));
            }
            catch (Exception msg)
            {
                ErrorViewModel errorViewModel = new ErrorViewModel();
                errorViewModel.RequestId = msg.Message;

                return View("_Error", errorViewModel);
            }
        }

        //ACTION PARA MOSTAR A LISTA DE PRODUTOS A SER GERIDA
        //public IActionResult ListaProdutosGestao()
        //{
        //    var search = _apiConnector.Get("Produtos");
        //    var result = JsonConvert.DeserializeObject<List<Produto>>(search);
        //    return View(result.ToList());
        //}

        public IActionResult GestaoProduto()
        {
            var search = _apiConnector.Get("Produtos");
            var result = JsonConvert.DeserializeObject<List<Produto>>(search);
            return View(result.ToList());
        }

        //ACTION COM MENU PARA ESCOLHER CRIAR PRODUTO OU IR PARA VIEW "ListaProdutosGestao" PARA EDITAR OU REMOVER PRODUTO
        public IActionResult MenuGestaoProduto()
        {
            return View();
        }

        private bool ProdutoExists(int id)
        {
            return bool.Parse(_apiConnector.GetById("Produto",id));
        }
    }
}
