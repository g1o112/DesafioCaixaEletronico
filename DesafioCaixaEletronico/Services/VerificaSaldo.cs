using DesafioCaixaEletronico.Models;
using DesafioCaixaEletronico.Services;
using System;

public class VerificaSaldo
{
    private readonly AtmService _atmService;

    public VerificaSaldo(AtmService atmService)
    {
        _atmService = atmService;
    }

    public string VerificarSaldo(Conta conta, decimal valorSaque)
    {
        // c o saldo valor d saque é maior q o saldo
        if (valorSaque > conta.Saldo)
        {
            return "Saldo insuficiente.";
        }

        DateTime horarioAtual = DateTime.Now; 

        // chamando o metodo de calcular notas do AtmService
        try
        {
            // valorSaque de decimal para int pois estava dando erro
            var notas = _atmService.CalcularNotas((int)valorSaque, horarioAtual);

            string resultado = "Saque realizado com sucesso! Notas: ";
            foreach (var nota in notas)
            {
                resultado += $"{nota.Value} nota(s) de R$ {nota.Key}, ";
            }
            conta.Saldo -= valorSaque; 
            return resultado.TrimEnd(',', ' '); // rmv espaço
        }
        catch (ArgumentException ex)
        {
            return ex.Message; 
        }
    }
}
