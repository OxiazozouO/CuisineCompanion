using System.Security.Cryptography;
using System.Text;

namespace CuisineCompanion.WebApi;

/// <summary>
/// 加密帮助
/// </summary>
public static class BackendEncryptionHelper
{
    public static void HasPassword(string password, string salt, out string hashpswd)
    {
        SHA256 sha256 = SHA256.Create();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(salt + password));
        hashpswd = Convert.ToBase64String(bytes);
    }
}