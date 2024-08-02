using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DesafioCaixaEletronico.Services
{
    public class AtmService
    {
        private readonly List<int> NotasDisponiveis = new List<int> { 50, 20, 10 };
        private readonly ContaService _contaService;

        public AtmService(ContaService contaService)
        {
            _contaService = contaService; 
        }

        public Dictionary<int, int> CalcularNotas(int valor, DateTime horario)
        {
            if (valor < 10 || valor % 10 != 0)
                throw new ArgumentException("O valor de saque deve ser múltiplo de 10 e maior ou igual a 10.");

            int limite = (horario.Hour >= 22 || horario.Hour < 6) ? 1000 : 10000;
            if (valor > limite)
                throw new ArgumentException($"O valor de saque não pode exceder R$ {limite} neste horário.");

            var resultado = new Dictionary<int, int>();

            foreach (var nota in NotasDisponiveis)
            {
                if (valor <= 0) break;

                int quantidade = valor / nota;

                if (quantidade > 0)
                {
                    resultado[nota] = quantidade;
                    valor -= quantidade * nota;
                }
            }

            if (valor > 0)
                throw new ArgumentException("Não é possível atender a este valor com as notas disponíveis.");

            return resultado;
        }

        public async Task<string> Sacar(string numeroConta, string senha, int valor)
        {
            // Verifica se o valor do saque é múltiplo de 10 e maior ou igual a 10
            if (valor < 10 || valor % 10 != 0)
                throw new ArgumentException("O valor de saque deve ser múltiplo de 10 e maior ou igual a 10.");

            // Obtém a conta
            var conta = await _contaService.ObterConta(numeroConta, senha);
            if (conta == null)
                throw new ArgumentException("Número da conta ou senha inválidos.");

            // Verifica se há saldo suficiente
            if (conta.Saldo < valor)
                throw new ArgumentException("Saldo insuficiente.");

            // Atualiza o saldo da conta
            await _contaService.AtualizarSaldo(numeroConta, conta.Saldo - valor);

            // Calcula as notas necessárias
            var notas = CalcularNotas(valor, DateTime.Now);

            // Retorna uma mensagem de sucesso com as notas
            return $"Saque realizado com sucesso. Notas: {string.Join(", ", notas.Select(n => $"{n.Value} nota(s) de R$ {n.Key}"))}.";
        }
    }
}
