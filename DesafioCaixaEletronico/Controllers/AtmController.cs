using Microsoft.AspNetCore.Mvc;
using DesafioCaixaEletronico.Services;
using DesafioCaixaEletronico.Controllers;

namespace DesafioCaixaEletronico.Controllers
{
    [Route("api/[controller]")] //Define a rota base para esse controlador... o [controller] sera substituído pelo nome da classe q no caso e a /api/atm.
    [ApiController]
    public class AtmController : ControllerBase
    {
        private readonly AtmService _atmService;

        public AtmController()
        {
            _atmService = new AtmService(); //chamando o atm service 
        }

        // metodo para calcular notas a partir de uma requisição HTTP POST
        [HttpPost("calcular")]
        public IActionResult CalcularNotas([FromBody] SaqueRequest request) //recebe a request sendo no saquerequest q tem o valor do saque
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
    }
}