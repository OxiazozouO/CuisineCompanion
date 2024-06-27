using System;
using System.Security.Cryptography;
using System.Text;
using CuisineCompanion.Helper;

namespace CuisineCompanion.Helpers;
/// <summary>
/// 加密帮助
/// </summary>
public static class FrontendEncryptionHelper
{
    public static void HasPassword(string password, out string salt, out string hashpswd)
    {
        SHA256 sha256 = SHA256.Create();
        salt = StringHelper.GetRandomString();
        byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(salt + password));
        hashpswd = Convert.ToBase64String(bytes);
    }
}