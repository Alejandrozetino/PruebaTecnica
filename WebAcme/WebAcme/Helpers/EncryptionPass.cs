using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;

namespace ACME.Helper;

public class EncryptionPass
{
    public static string HashPassword(string password)
    {
        // Genera un hash de contraseña usando Bcrypt
        string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);

        return hashedPassword;
    }

    public static bool VerifyPassword(string hashedPassword, string candidatePassword)
    {
        // Verifica si la contraseña ingresada coincide con el hash almacenado
        bool passwordMatches = BCrypt.Net.BCrypt.Verify(candidatePassword, hashedPassword);

        return passwordMatches;
    }
}
