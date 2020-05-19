using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Service.Services;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AutorController : ControllerBase
    {
        private AutorService<Autor> autorService = new AutorService<Autor>();
        
        [HttpGet]
        [Route("ObterAutoresAsync/{quantidadeListaAutores}")]
        public async Task<IActionResult> ObterAutoresAsync(int quantidadeListaAutores)
        {
            try
            {
                return new ObjectResult(await autorService.SelectByQuantidadeAsync(quantidadeListaAutores));
            }
            catch (Exception ex)
            {
                return BadRequest(ex);
            }
        }
    }
}