using Microsoft.AspNetCore.Mvc;
using DesafioCaixaEletronico.Models;
using DesafioCaixaEletronico.Services;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace DesafioCaixaEletronico.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContaController : ControllerBase
    {
        private readonly ContaService _contaService;

        public ContaController(ContaService contaService)
        {
            _contaService = contaService; // Injetando o serviço de conta
        }

        // Criar conta
        [HttpPost("criar")]
        public async Task<IActionResult> CriarConta([FromBody] Conta novaConta)
        {
            if (novaConta == null)
            {
                return BadRequest(new ResponseMessage { Mensagem = "Conta não pode ser nula." });
            }

            // Adicionar lógica para garantir que o Nome não seja nulo
            if (string.IsNullOrEmpty(novaConta.Nome))
            {
                return BadRequest(new ResponseMessage { Mensagem = "O campo Nome é obrigatório." });
            }

            try
            {
                Console.WriteLine($"Criando conta: Nome={novaConta.Nome}, Saldo={novaConta.Saldo}, NumeroConta={novaConta.NumeroConta}");
                await _contaService.CriarConta(novaConta);
                return Ok(new ResponseMessage { Mensagem = "Conta criada com sucesso!", Saldo = novaConta.Saldo });
            }
            catch (MongoException mongoEx)
            {
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao conectar com o banco de dados: " + mongoEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao criar conta: {ex.Message}");
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao criar conta: " + ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            if (loginRequest == null || string.IsNullOrEmpty(loginRequest.NumeroConta) || string.IsNullOrEmpty(loginRequest.Senha))
            {
                return BadRequest(new ResponseMessage { Mensagem = "Número da conta e senha são obrigatórios." });
            }

            Console.WriteLine($"Número da Conta: {loginRequest.NumeroConta}, Senha: {loginRequest.Senha}"); // ver c encontrou certinho no console

            try
            {
                var conta = await _contaService.ObterConta(loginRequest.NumeroConta, loginRequest.Senha);
                if (conta == null)
                {
                    return BadRequest(new ResponseMessage { Mensagem = "Número da conta ou senha inválidos." });
                }

                return Ok(new ResponseMessage { Mensagem = $"Login feito com sucesso! Bem-vindo, {conta.Nome}!", Saldo = conta.Saldo });
            }
            catch (MongoException mongoEx)
            {
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao conectar com o banco de dados: " + mongoEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao fazer login: {ex.Message}");
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao fazer login: " + ex.Message });
            }
        }
        [HttpGet("saldo")]
        public async Task<IActionResult> ObterSaldo([FromQuery] string numeroConta, [FromQuery] string senha)
        {
            if (string.IsNullOrEmpty(numeroConta) || string.IsNullOrEmpty(senha))
            {
                return BadRequest(new ResponseMessage { Mensagem = "Número da conta e senha são obrigatórios." });
            }

            try
            {
                var conta = await _contaService.ObterConta(numeroConta, senha);
                if (conta == null)
                {
                    return BadRequest(new ResponseMessage { Mensagem = "Número da conta ou senha inválidos." });
                }

                return Ok(new ResponseMessage { Mensagem = "Saldo obtido com sucesso!", Saldo = conta.Saldo });
            }
            catch (MongoException mongoEx)
            {
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao conectar com o banco de dados: " + mongoEx.Message });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao obter saldo: {ex.Message}");
                return StatusCode(500, new ResponseMessage { Mensagem = "Erro ao obter saldo: " + ex.Message });
            }
        }

    }
}
