using Microsoft.AspNetCore.Mvc;
using Models;
using System;
using WebApiFrutaria.Business;

namespace WebApiFrutaria.Controllers
{
    [ApiVersion("1")]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly IFuncionarioBusiness FuncionarioBusiness;

        public FuncionariosController(IFuncionarioBusiness funcionarioBusiness)
        {
            FuncionarioBusiness = funcionarioBusiness;
        }

        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                return Ok(FuncionarioBusiness.FindAll());
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
                var result = FuncionarioBusiness.FindById(id);
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
        public IActionResult Post([FromBody] Funcionario funcionario)
        {
            try
            {
                var result = FuncionarioBusiness.Create(funcionario);
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
        public IActionResult Put([FromBody] Funcionario funcionario)
        {
            try
            {
                if (funcionario == null) return BadRequest();
                var result = FuncionarioBusiness.Update(funcionario);
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
                var result = FuncionarioBusiness.Delete(id);
                if (result == true)
                {
                    return Ok();
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
