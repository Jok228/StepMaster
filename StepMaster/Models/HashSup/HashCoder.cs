using System.Security.Cryptography;
using System.Text;

namespace StepMaster.Models.HashSup;

public class HashCoder
{
    public static string GetHash(string password)
    {
        byte[] salt = GenerateSalt();
        byte[] md5Hash = GenerateMD5Hash(password, salt);
        string savedPasswordHash = Convert.ToBase64String(md5Hash);
        return savedPasswordHash;
    }
    private static byte[] GenerateMD5Hash(string password, byte[] salt)
    {
        byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
        byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];

        using var hash = new MD5CryptoServiceProvider();

        return hash.ComputeHash(saltedPassword);
    }
    private static byte[] GenerateSalt()
    {
        const int SaltLength = 64;
        byte[] salt = new byte[SaltLength];

        var rngRand = new RNGCryptoServiceProvider();
        rngRand.GetBytes(salt);

        return salt;
    }
}