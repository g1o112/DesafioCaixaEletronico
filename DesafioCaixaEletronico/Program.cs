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


// configurar solicitação HTTP... mostrar os erros completos
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseHttpsRedirection();
app.UseStaticFiles(); // ele permite q a aplicação sirva arquivos os estáticos da pasta wwwroot (css, html js, etc)
app.UseRouting(); //configura o roteamento
app.UseAuthorization();
app.MapControllers();


// rota padrão para arquivos os estáticos
app.MapGet("/", () => Results.Redirect("/index.html"));

app.Run();