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
        public int UserId { get; set; }
        public string Auth { get; set; } = string.Empty;
        public SettingsViewModel()
        {
            LogOut = new Command(ToLoginPage);
            DeleteAccount = new Command(RemoveAccount); 
            if (SecureStorage.Default.GetAsync("Token").Result == null)
            {
                SecureStorage.Default.Remove("UserId");
                SecureStorage.Default.Remove("Token");
                Application.Current!.MainPage!.DisplayAlert("Hiba", "Nincs bejelentkezve!", "OK");
                Application.Current!.MainPage = new LoginPage();
                return;
            }
            UserId = Convert.ToInt32(SecureStorage.Default.GetAsync("UserId").Result);
            Auth = SecureStorage.Default.GetAsync("Token").Result!;
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
