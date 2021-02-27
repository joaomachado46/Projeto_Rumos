using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebApiFrutaria.Business;

namespace WebApiFrutaria.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class CarrinhoComprasController : ControllerBase
    {
        private readonly ICarrinhoComprasBusiness CarrinhoCompraBusiness;

        public CarrinhoComprasController(ICarrinhoComprasBusiness carrinhoCompraBusiness)
        {
            CarrinhoCompraBusiness = carrinhoCompraBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(CarrinhoCompraBusiness.FindAll());
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
                var result = CarrinhoCompraBusiness.FindById(id);
                if (result == null) return NoContent();
                return Ok(result);
            }
            catch (Exception msg)
            {
                return BadRequest("ERROR IN PROCESS THE REQUEST" + msg.Message);
            }
        }

        // POST api/<ApiProdutosController>
        [HttpPost]
        public IActionResult Post([FromBody] CarrinhoCompra Carrinho)
        {
            try
            {
                var result = CarrinhoCompraBusiness.Create(Carrinho);
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
        public IActionResult Put([FromBody] CarrinhoCompra Carrinho)
        {
            try
            {
                if (Carrinho == null) return BadRequest();
                var result = CarrinhoCompraBusiness.Update(Carrinho);
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
                var result = CarrinhoCompraBusiness.Delete(id);
                if (result == true)
                {
                    return NoContent();
                }
                else
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
