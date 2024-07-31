using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

public class CorsConfig
{
    public static void ConfigureCors(IServiceCollection services)
    {
        Console.WriteLine("Configurando CORS!!"); //confirmando q o metodo esta sendo chamade

        services.AddCors(options =>
        {
            options.AddPolicy("AllowSpecificOrigin",
                builder =>
                {
                    builder.WithOrigins("https://localhost:7263")
                           .AllowAnyMethod()
                           .AllowAnyHeader()
                           .AllowCredentials();
                });
        });
    }
}