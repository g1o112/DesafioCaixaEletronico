using Microsoft.AspNetCore.Mvc;
using DesafioCaixaEletronico.Services;
using DesafioCaixaEletronico.Models;

namespace DesafioCaixaEletronico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtmController : ControllerBase
    {
        private readonly AtmService _atmService;
        private readonly ContaService _contaService; // Adicione esta linha para injetar o ContaService

        public AtmController(AtmService atmService, ContaService contaService)
        {
            _atmService = atmService;
            _contaService = contaService; // Inicialize o ContaService
        }

        [HttpPost("calcular")]
        public IActionResult CalcularNotas([FromBody] SaqueRequest request)
        {
            try
            {
                var resultado = _atmService.CalcularNotas(request.Valor, DateTime.Now);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("sacar")]
        public async Task<IActionResult> Sacar([FromBody] SaqueRequest request)
        {
            try
            {
                var resultado = await _atmService.Sacar(request.NumeroConta!, request.Senha!, request.Valor);
                return Ok(resultado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
