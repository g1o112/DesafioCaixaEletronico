using DesafioCaixaEletronico.Models;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;

namespace DesafioCaixaEletronico
{
    public class DesafioCaixaEletronicoContext
    {
        private readonly IMongoDatabase _database;

        public DesafioCaixaEletronicoContext(IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("MongoDb");
            var client = new MongoClient(connectionString);
            _database = client.GetDatabase("DesafioCaixaEletronicoDB");
        }

        public IMongoCollection<Conta> Contas => _database.GetCollection<Conta>("Contas");
    }
}
