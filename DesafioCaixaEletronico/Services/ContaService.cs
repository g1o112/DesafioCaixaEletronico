using MongoDB.Bson;
using MongoDB.Driver;
using DesafioCaixaEletronico.Models;
using System;
using System.Threading.Tasks;

namespace DesafioCaixaEletronico.Services
{
    public class ContaService
    {
        private readonly IMongoCollection<Conta> _contas;

        public ContaService(IConfiguration configuration)
        {
            var client = new MongoClient(configuration.GetConnectionString("MongoDb"));
            var database = client.GetDatabase("DesafioCaixaEletronicoDB");
            _contas = database.GetCollection<Conta>("Contas");

            // Verifica se a conexão foi bem-sucedida
            try
            {
                var result = database.RunCommandAsync((Command<BsonDocument>)"{ ping: 1 }").Result;
                Console.WriteLine("Conexão com o banco de dados estabelecida com sucesso.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erro ao conectar ao banco de dados: {ex.Message}");
            }
        }

        public async Task CriarConta(Conta novaConta)
        {
            if (novaConta == null)
            {
                throw new ArgumentNullException(nameof(novaConta), "A nova conta não pode ser nula.");
            }

            // Verifica se já existe uma conta com o mesmo número de conta
            var contaExistente = await _contas.Find(c => c.NumeroConta == novaConta.NumeroConta).FirstOrDefaultAsync();
            if (contaExistente != null)
            {
                throw new InvalidOperationException("Uma conta com esse número já existe.");
            }

            await _contas.InsertOneAsync(novaConta);
        }

        // Atualiza o saldo da conta
        public async Task AtualizarSaldo(string numeroConta, decimal novoSaldo)
        {
            if (novoSaldo < 0)
            {
                throw new ArgumentException("O saldo não pode ser negativo.");
            }

            var filtro = Builders<Conta>.Filter.Eq(c => c.NumeroConta, numeroConta);
            var atualizacao = Builders<Conta>.Update.Set(c => c.Saldo, novoSaldo);
            await _contas.UpdateOneAsync(filtro, atualizacao);
        }

        public async Task<Conta?> ObterConta(string numeroConta, string senha)
        {
            if (string.IsNullOrEmpty(numeroConta) || string.IsNullOrEmpty(senha))
            {
                throw new ArgumentException("Número da conta e senha não podem ser nulos ou vazios.");
            }

            return await _contas.Find(c => c.NumeroConta == numeroConta && c.Senha == senha).FirstOrDefaultAsync();
        }
    }
}
