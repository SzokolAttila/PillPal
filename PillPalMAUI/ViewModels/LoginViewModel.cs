using PillPalLib.APIHandlers;
using PillPalLib.DTOs.UserDTOs;
using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class LoginViewModel : ViewModelBase
    {
        private readonly UserAPIHandler _userHandler;
        private string username = string.Empty;
        private string password = string.Empty;
        public ICommand Login {  get; private set; }
        public string Username
        {
            get => username;
            set
            {
                username = value; 
                Changed();
            }
        }
        public string Password
        {
            get => password;
            set
            {
                password = value; 
                Changed();
            }
        }
        public LoginViewModel()
        {
            _userHandler = new UserAPIHandler();
            Login = new Command(LogIntoAccount);
        }
        private async void LogIntoAccount()
        {
            try
            {
                var login = _userHandler.Login(new CreateUserDto() { UserName = username, Password = password });
                if (Username == "administrator"){
                    Application.Current!.MainPage = new AdminUsersPage(login.Token);
                }
                else{
                    Application.Current!.MainPage = new MainPage(login.Id, login.Token);
                }
            }
            catch (Exception ex) 
            {
                await Application.Current!.MainPage!.DisplayAlert("Sikertelen bejelentkezés!",
                    $"A megadott felhasználónév vagy jelszó helytelen. ({ex.Message})", "OK");
            }
        }
    }
}
