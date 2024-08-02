using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.Text.Json.Serialization;

namespace DesafioCaixaEletronico.Models
{
    [JsonSerializable(typeof(Conta))] // Adiciona o atributo aqui
    public class Conta
    {
        [BsonId] //definindo tudo como nao nulo pois sem esta dando erro
        public ObjectId Id { get; set; }
        public string Nome { get; set; } = string.Empty; 
        public string Senha { get; set; } = string.Empty; 
        public decimal Saldo { get; set; } = 0; 
        public string NumeroConta { get; set; } = string.Empty; 
    }
}
