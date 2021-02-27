using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApiFrutaria.Business;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiFrutaria.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class ProdutosController : ControllerBase
    {
        private readonly IProductBusiness ProductBusiness;

        public ProdutosController(IProductBusiness product)
        {
            ProductBusiness = product;
        }

        // GET: api/<ApiProdutosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(ProductBusiness.FindAll());
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }

        // GET api/<ApiProdutosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                var result = ProductBusiness.FindById(id);
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }

        [HttpPost]
        [Route("saveimage")]
        public IActionResult SaveImageAzureBlob([FromBody]string files)
        {
            try
            {
                if (files == null) return BadRequest();
                var result = ProductBusiness.SaveImageAzureBlob(files);
                if (result == null) return NoContent();
                return Ok(result.ToString());
            }
            catch (Exception msg)
            {
                return BadRequest(msg.Message);
            }
        }
        // POST api/<ApiProdutosController>
        [HttpPost]
        public IActionResult Post([FromBody] Produto value)
        {
            try
            {
                var result = ProductBusiness.Create(value);
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }

        // PUT api/<ApiProdutosController>/5
        [HttpPut]
        public IActionResult Put([FromBody] Produto produto)
        {
            try
            {
                if (produto == null) return BadRequest();
                var result = ProductBusiness.Update(produto);
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }

        // DELETE api/<ApiProdutosController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                var result = ProductBusiness.Delete(id);
                if (result == true)
                {
                    return NoContent();
                } else
                {
                    return BadRequest();
                }
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }
    }
}
