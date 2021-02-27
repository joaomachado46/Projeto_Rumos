using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebApiFrutaria.Business;

namespace WebApiFrutaria.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsuariossController : ControllerBase
    {
        private readonly IUsuarioBusiness UsuarioBusiness;

        public UsuariossController(IUsuarioBusiness usuarioBusiness)
        {
            UsuarioBusiness = usuarioBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(UsuarioBusiness.FindAll());
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
                var result = UsuarioBusiness.FindById(id);
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
        public IActionResult Post([FromBody] Usuario Usuario)
        {
            try
            {
                var result = UsuarioBusiness.Create(Usuario);
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
        public IActionResult Put([FromBody] Usuario Usuario)
        {
            try
            {
                if (Usuario == null) return BadRequest();
                var result = UsuarioBusiness.Update(Usuario);
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
                var result = UsuarioBusiness.Delete(id);
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
