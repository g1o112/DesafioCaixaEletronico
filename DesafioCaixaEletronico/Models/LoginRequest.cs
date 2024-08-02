using System.Text.Json.Serialization;

namespace DesafioCaixaEletronico.Models
{
    //[JsonSerializable(typeof(LoginRequest))]
    public class LoginRequest
    {
        public string NumeroConta { get; set; }
        public string Senha { get; set; }
    }
}
