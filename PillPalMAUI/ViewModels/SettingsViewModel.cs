using PillPalLib.APIHandlers;
using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class SettingsViewModel : ViewModelBase
    {
        private readonly UserAPIHandler handler = new();
        private HomeButtonViewModel homeButton;
        public HomeButtonViewModel HomeButton
        {
            get => homeButton; 
            set
            {
                homeButton = value;
                Changed();
            }
        }
        public int UserId { get; set; }
        public string Auth { get; set; } = string.Empty;
        public SettingsViewModel(int userId, string auth)
        {
            UserId = userId;
            Auth = auth;
            HomeButton = new HomeButtonViewModel(userId, auth);
            LogOut = new Command(ToLoginPage);
            DeleteAccount = new Command(RemoveAccount);
        }
        public ICommand LogOut { get; private set; }
        public ICommand DeleteAccount { get; private set; }
        private async void ToLoginPage()
        {
            if (await Application.Current!.MainPage!.DisplayAlert("Kijelentkezés", "Biztosan ki akar jelentkezni?", "Igen", "Mégse"))
            {
                Application.Current!.MainPage = new LoginPage();
                await Application.Current.MainPage.DisplayAlert("Kijelentkezve", "Sikeres kijelentkezés!", "OK");
            }
        }
        private async void RemoveAccount()
        {
            if(await Application.Current!.MainPage!.DisplayAlert("Fiók törlése", "Biztosan törölni akarja a fiókját? Ez a művelet végleges.", "Igen", "Mégse"))
            {
                handler.DeleteUser(UserId, Auth);
                Application.Current!.MainPage = new LoginPage();
                await Application.Current.MainPage.DisplayAlert("Törölt fiók", "Fiók sikeresen törölve.", "OK");
            }
        }
    }
}
