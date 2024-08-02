using DesafioCaixaEletronico.Controllers;
using DesafioCaixaEletronico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace DesafioCaixaEletronico
{
    [JsonSerializable(typeof(LoginRequest))] // Correto
    [JsonSerializable(typeof(Dictionary<int, int>))]
    [JsonSerializable(typeof(SaqueRequest))]
    [JsonSerializable(typeof(Conta))]
    [JsonSerializable(typeof(ResponseMessage))]
    [JsonSerializable(typeof(ValidationProblemDetails))]
    internal partial class AppJsonSerializerContext : JsonSerializerContext
    {
    }
}
