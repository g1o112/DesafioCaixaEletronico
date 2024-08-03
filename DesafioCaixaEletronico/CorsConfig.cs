using Microsoft.Extensions.DependencyInjection;

public class CorsConfig
{
    public static void ConfigureCors(IServiceCollection services)
    {
        Console.WriteLine("Configurando CORS!!"); // Confirmando que o método está sendo chamado

        services.AddCors(options =>
        {
            // Política para permitir uma origem específica (por exemplo, a URL do frontend Angular)
            options.AddPolicy("AllowAngularApp",
                builder =>
                {
                    builder.WithOrigins("http://localhost:51965") // URL do frontend Angular
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
    }
}
