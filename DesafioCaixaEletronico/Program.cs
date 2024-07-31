using DesafioCaixaEletronico;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = WebApplication.CreateBuilder(args); //permitindo passar os parametros 

CorsConfig.ConfigureCors(builder.Services);


builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.TypeInfoResolver = AppJsonSerializerContext.Default;
    });

var app = builder.Build();


// configurar solicita��o HTTP... mostrar os erros completos
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ele permite q a aplica��o sirva arquivos os est�ticos da pasta wwwroot (css, html js, etc)
app.UseRouting(); //configura o roteamento
app.UseAuthorization();
app.MapControllers();


// rota padr�o para arquivos os est�ticos
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();