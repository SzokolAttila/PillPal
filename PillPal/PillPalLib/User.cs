using System.Security.Cryptography;
using System.Text;

namespace PillPalLib
{
    public class User 
    {
        public User(string userName, string password)
        {
            UserName = userName;
            PasswordText = password;
        }
        private const int HASH_SIZE = 512;
        private byte[] Salt()
        {
            var salt = "";
            for (int i = 1; i < UserName.Length; ++i)
            {
                salt += char.GetNumericValue(UserName[i - 1]).ToString() + (char.GetNumericValue(UserName[i - 1]) * char.GetNumericValue(UserName[i]) / UserName.Length).ToString();
            }
            return Encoding.UTF8.GetBytes(salt);
        }
        private int Iteration => (int)(Math.Pow(UserName.Length, 6) / Math.Pow(UserName.Length - 4, 3));
        public string UserName { get; init; }
        public byte[] Password { get; private set; }
        public string PasswordText { 
            set{
                Password = new Rfc2898DeriveBytes(value, Salt(), Iteration).GetBytes(HASH_SIZE);
            }
        }

        public bool Matches(byte[] hashedPassword)
        {
            return hashedPassword.Length == Password.Length
                && Enumerable.Range(0, hashedPassword.Length).All(i => hashedPassword[i] == Password[i]);
        }
    }
}
