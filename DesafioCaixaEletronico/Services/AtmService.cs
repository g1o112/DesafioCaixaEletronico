using System;
using System.Collections.Generic;

namespace DesafioCaixaEletronico.Services
{
    public class AtmService
    {
        private readonly List<int> NotasDisponiveis = new List<int> { 50, 20, 10 };

        public Dictionary<int, int> CalcularNotas(int valor, DateTime horario)
        {
            if (valor < 10 || valor % 10 != 0)
                throw new ArgumentException("O valor de saque deve ser múltiplo de 10 e maior ou igual a 10.");

            int limite = (horario.Hour >= 22 || horario.Hour < 6) ? 1000 : 10000;//? definindo valor d limite para as condicoes
            if (valor > limite)
                throw new ArgumentException($"O valor de saque não pode exceder R$ {limite} neste horário.");

            var resultado = new Dictionary<int, int>();

            // calcula a quantidade de notas necessárias
            foreach (var nota in NotasDisponiveis)
            {
                if (valor <= 0) break;

                // verifica quantas notas são necessárias
                int quantidade = valor / nota;

                if (quantidade > 0)
                {
                    resultado[nota] = quantidade; // adc a quantidade de notas ao resultado
                    valor -= quantidade * nota; // reduz o valor restante
                }
            }

            // verifica se atendeu ao valor solicitado
            if (valor > 0)
                throw new ArgumentException("Não é possível atender a este valor com as notas disponíveis.");

            return resultado; // retorna a quantidade de notas
        }
    }
}