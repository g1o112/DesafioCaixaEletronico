using System.Text.Json.Serialization;

namespace DesafioCaixaEletronico.Controllers
{
    [JsonSerializable(typeof(SaqueRequest))]
    public class SaqueRequest
    {

        public int Valor { get; set; } // 'Valor' com letra maiúscula
    }
}