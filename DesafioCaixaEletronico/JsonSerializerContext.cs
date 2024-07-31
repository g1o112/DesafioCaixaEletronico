using DesafioCaixaEletronico.Controllers;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DesafioCaixaEletronico
{
    [JsonSerializable(typeof(Dictionary<int, int>))]
    [JsonSerializable(typeof(SaqueRequest))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}