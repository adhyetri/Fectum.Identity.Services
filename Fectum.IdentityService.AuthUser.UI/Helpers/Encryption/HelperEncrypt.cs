using System.Security.Cryptography;
using System.Text;

namespace Fectum.IdentityService.AuthUser.UI.Helpers.Encryption
{
    public class HelperEncrypt
    {
        private const int KeySize = 16;
        public static string Encrpyt(string password)
        {
            using RandomNumberGenerator rng = RandomNumberGenerator.Create();
            byte[] salt = new byte[KeySize];
            rng.GetBytes(salt);

            // combine password and salt
            byte[] bytepassword = Encoding.UTF8.GetBytes(password);
            byte[] bytecombine = new byte[bytepassword.Length + salt.Length];

            Array.Copy(salt, 0, bytecombine, 0, salt.Length);
            Array.Copy(bytepassword, 0, bytecombine, salt.Length, bytepassword.Length);

            byte[] sand = SHA256.HashData(bytecombine);

            byte[] haspassandsalt = new byte[KeySize + sand.Length];

            Array.Copy(bytepassword, 0, haspassandsalt, 0, KeySize);
            Array.Copy(sand, 0, haspassandsalt, KeySize, sand.Length);

            return Convert.ToBase64String(haspassandsalt);
        }
    }
}
