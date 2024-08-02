using DesafioCaixaEletronico;
using DesafioCaixaEletronico.Models;
using DesafioCaixaEletronico.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args);

// Configura��o do CORS
CorsConfig.ConfigureCors(builder.Services);

// Adicionando servi�os
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
    });

builder.Services.AddSingleton<IConfiguration>(builder.Configuration);
builder.Services.AddScoped<ContaService>();
builder.Services.AddScoped<AtmService>(); // Adicionando o AtmService
builder.Services.AddScoped<VerificaSaldo>(); // Registrando VerificaSaldo


var app = builder.Build();

// Configurar solicita��o HTTP
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllers();
app.MapGet("/", () => Results.Redirect("/index.html"));
app.Run();
