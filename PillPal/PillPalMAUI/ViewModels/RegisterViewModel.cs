using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PillPalMAUI.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
        private const int MIN_USERNAME_LENGTH = 6;
        private const int MAX_USERNAME_LENGTH = 20;
        private const int MIN_PASSWORD_LENGTH = 8;
        private const int MAX_SECURITY_LEVEL = 15;
        //defining the value of each security level
        private const int PASSWORD_LENGTH = 8;
        private const int PASSWORD_SPECIALS = 4;
        private const int PASSWORD_NUMBERS = 2;
        private const int PASSWORD_BOTH_CASES = 1;
        public RegisterViewModel() 
        {
            passwordTooShort = errorText;
            passwordNumber = errorText;
            passwordBothCases = errorText;
            passwordSpecial = errorText;
        }
        private readonly Color errorText = (Color)Application.Current!.Resources["ErrorText"];
        private readonly Color acceptText = (Color)Application.Current!.Resources["AcceptText"];
        // binding the color of password security labels
        private Color passwordTooShort;
        private Color passwordSpecial;
        private Color passwordNumber;
        private Color passwordBothCases;
        // other error message labels (visibility)
        private bool passwordsDoNotMatch = false;
        private bool usernameTaken = false;
        private bool usernameIncorrectLength = true;
        private string username = string.Empty;
        private string password = string.Empty;
        private string passwordAgain = string.Empty;
        public bool UsernameIncorrectLength
        {
            get => usernameIncorrectLength;
            set
            {
                usernameIncorrectLength = value;
                Changed();
            }
        }
        public Color PasswordTooShort
        {
            get => passwordTooShort;
            set
            {
                passwordTooShort = value;
                Changed();
            }
        }
        public Color PasswordSpecial
        {
            get => passwordSpecial;
            set
            {
                passwordSpecial = value;
                Changed();
            }
        }
        public Color PasswordBothCases
        {
            get => passwordBothCases;
            set
            {
                passwordBothCases = value;
                Changed();
            }
        }
        public Color PasswordNumber
        {
            get => passwordNumber;
            set
            {
                passwordNumber = value;
                Changed();
            }
        }
        public bool PasswordsDoNotMatch
        {
            get => passwordsDoNotMatch;
            set
            {
                passwordsDoNotMatch = value;
                Changed();
            }
        }
        public bool UsernameTaken
        {
            get => usernameTaken;
            set
            {
                usernameTaken = value;
                Changed();
            }
        }
        public string Username
        {
            get => username;
            set
            {
                username = value;
                Changed();
                if (username.Length >= MIN_USERNAME_LENGTH && username.Length <= MAX_USERNAME_LENGTH)
                    UsernameIncorrectLength = false;
                else UsernameIncorrectLength = true;
                CheckValidity();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value;
                Changed();
                CheckSecurity();
                CheckPasswordMatch();
                CheckValidity();
            }
        }
        public string PasswordAgain
        {
            get => passwordAgain;
            set
            {
                passwordAgain = value;
                Changed();
                CheckPasswordMatch();
                CheckValidity();
            }
        }
        private bool isEnabled = false;
        public bool IsEnabled
        {
            get => isEnabled;
            set
            {
                isEnabled = value;
                Changed();
            }
        }
        private void CheckPasswordMatch()
        {
            if (password != passwordAgain)
                PasswordsDoNotMatch = true;
            else PasswordsDoNotMatch = false;
        }
        private void CheckSecurity()
        {
            var securityLevel = GetSecurityLevel(Password);
            if (securityLevel >= PASSWORD_LENGTH)
            {
                PasswordTooShort = acceptText;
                securityLevel -= PASSWORD_LENGTH;
            }
            else PasswordTooShort = errorText;
            if (securityLevel >= PASSWORD_SPECIALS)
            {
                PasswordSpecial = acceptText;
                securityLevel -= PASSWORD_SPECIALS;
            }
            else PasswordSpecial = errorText;
            if (securityLevel >= PASSWORD_NUMBERS)
            {
                PasswordNumber = acceptText;
                securityLevel -= PASSWORD_NUMBERS;
            }
            else PasswordNumber = errorText;
            if (securityLevel >= PASSWORD_BOTH_CASES)
            {
                PasswordBothCases = acceptText;
                securityLevel -= PASSWORD_BOTH_CASES;
            }
            else PasswordBothCases = errorText;
        }
        private static int GetSecurityLevel(string password)
        {
            var securityLevel = 0;
            if (Regex.IsMatch(password, "(?=.*[a-z])") && Regex.IsMatch(password, "(?=.*[A-Z])"))
                securityLevel += 1;
            if (Regex.IsMatch(password, "(?=.*[0-9])"))
                securityLevel += 2;
            if (Regex.IsMatch(password, "(?=.*[@,$,!,%,*,?,&])"))
                securityLevel += 4;
            if (password.Length >= MIN_PASSWORD_LENGTH)
                securityLevel += 8;
            return securityLevel;
        }
        private void CheckValidity()
        {
            if (!UsernameIncorrectLength && GetSecurityLevel(password) == MAX_SECURITY_LEVEL && password == passwordAgain)
                IsEnabled = true;
            else IsEnabled = false;
        }
    }
}
