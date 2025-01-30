using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    class RegisterViewModel : ViewModelBase
    {
        private readonly UserAPIHandler _handler;
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
            _handler = new UserAPIHandler();
            Register = new Command(PostUser, IsValid);
            passwordTooShort = GetErrorColor();
            passwordNumber = GetErrorColor();
            passwordBothCases = GetErrorColor();
            passwordSpecial = GetErrorColor();
        }
        private bool IsValid()
        {
            return !UsernameIncorrectLength && GetSecurityLevel(password) == MAX_SECURITY_LEVEL && password == passwordAgain;
        }
        private async void PostUser()
        {
            var newUser = new CreateUserDto()
            {
                UserName = username,
                Password = password,    
            };
            try
            {
                _handler.CreateUser(newUser);
                await Application.Current!.MainPage!.DisplayAlert("Sikeres regisztráció!", "Most átirányítunk a bejelentkezéshez.", "OK");
                Application.Current.MainPage = new LoginPage();

            }
            catch (Exception ex) 
            {
                UsernameTaken = true;
            }
        }
        private static Color GetErrorColor()
        {
            return (Color)Application.Current!.Resources["ErrorText"];
        }
        private static Color GetAcceptColor()
        {
            return (Color)Application.Current!.Resources["AcceptText"];
        }
        public ICommand Register { get; private set; }
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
                (Register as Command)!.ChangeCanExecute();
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
                (Register as Command)!.ChangeCanExecute();
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
                (Register as Command)!.ChangeCanExecute();
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
                PasswordTooShort = GetAcceptColor();
                securityLevel -= PASSWORD_LENGTH;
            }
            else PasswordTooShort = GetErrorColor();
            if (securityLevel >= PASSWORD_SPECIALS)
            {
                PasswordSpecial = GetAcceptColor();
                securityLevel -= PASSWORD_SPECIALS;
            }
            else PasswordSpecial = GetErrorColor();
            if (securityLevel >= PASSWORD_NUMBERS)
            {
                PasswordNumber = GetAcceptColor();
                securityLevel -= PASSWORD_NUMBERS;
            }
            else PasswordNumber = GetErrorColor();
            if (securityLevel >= PASSWORD_BOTH_CASES)
            {
                PasswordBothCases = GetAcceptColor();
                securityLevel -= PASSWORD_BOTH_CASES;
            }
            else PasswordBothCases = GetErrorColor();
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
    }
}
