using System;
using System.Security.Cryptography;
using System.Text;

public static class Hashing
{
    // Cria um hash a partir da senha
    public static string HashPassword(string password)
    {
        using var sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

        // Converte os bytes para uma representação hexadecimal
        var sb = new StringBuilder();
        foreach (var b in bytes)
        {
            sb.Append(b.ToString("x2")); // "x2" converte bytes para hexadecimal com 2 dígitos
        }
        return sb.ToString();
    }

    // Verifica se a senha fornecida corresponde ao hash armazenado
    public static bool VerifyPassword(string password, string storedHash)
    {
        var passwordHash = HashPassword(password);
        return passwordHash == storedHash;
    }
}
