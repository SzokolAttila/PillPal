using PillPalLib.Interfaces;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json.Serialization;

namespace PillPalLib
{
    public class User : IIdentified
    {
        public User(string userName, string password)
        {
            UserName = userName;
            PasswordText = password;
        }
        [JsonConstructor]
        public User(int id, string userName, byte[] password)
        {
            Id = id;
            UserName = userName;
            Password = password;
        }
        public User(int id, string userName, string password)
        {
            Id = id;
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

        public int Id { get; set; }
        public bool Matches(byte[] password)
        {
            return password.Length == Password.Length
                && Enumerable.Range(0, password.Length).All(i => password[i] == Password[i]);
        }
        public bool Matches(string password)
        {
            byte[] hashedPassword = new Rfc2898DeriveBytes(password, Salt(), Iteration).GetBytes(HASH_SIZE);
            return hashedPassword.Length == Password.Length
                && Enumerable.Range(0, hashedPassword.Length).All(i => hashedPassword[i] == Password[i]);
        }
    }
}
