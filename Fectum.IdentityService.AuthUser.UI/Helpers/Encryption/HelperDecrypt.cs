using System.Security.Cryptography;
using System.Text;

namespace Fectum.IdentityService.AuthUser.UI.Helpers.Encryption
{
    public class HelperDecrypt
    {
        private const int KeySize = 16;
        public static bool IsValidPassword(string enteredPassword, string storedPasswordHash)
        {
            try
            {
                byte[] storedHashBytes = Convert.FromBase64String(storedPasswordHash);

                byte[] salt = new byte[KeySize];
                Array.Copy(storedHashBytes, 0, salt, 0, KeySize);

                byte[] bytePassword = Encoding.UTF8.GetBytes(enteredPassword);
                byte[] byteCombine = new byte[bytePassword.Length + salt.Length];

                Array.Copy(salt, 0, byteCombine, 0, salt.Length);
                Array.Copy(bytePassword, 0, byteCombine, salt.Length, bytePassword.Length);

                byte[] sand = SHA256.HashData(byteCombine);

                byte[] hasPassAndSalt = new byte[KeySize + sand.Length];

                Array.Copy(salt, 0, hasPassAndSalt, 0, KeySize);
                Array.Copy(sand, 0, hasPassAndSalt, KeySize, sand.Length);

                string computedHash = Convert.ToBase64String(hasPassAndSalt);

                return computedHash == storedPasswordHash;
            }
            catch (FormatException ex)
            {
                throw new Exception(ex.Message);
            }
        }

    }
}
