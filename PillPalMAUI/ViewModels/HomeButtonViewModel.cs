using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class HomeButtonViewModel : ViewModelBase
    {
        public HomeButtonViewModel()
        {
            NewReminderPressed = new Command(ToNewReminder);
            SettingsPressed = new Command(ToSettings);
            HomePressed = new Command(ToHome);
        }
        public ICommand NewReminderPressed { get; private set; }
        public ICommand SettingsPressed { get; private set; }
        public ICommand HomePressed { get; private set; }
        private void ToHome()
        {
            Application.Current!.MainPage = new MainPage();
        }
        private void ToSettings()
        {
            Application.Current!.MainPage = new SettingsPage();
        }
        private void ToNewReminder()
        {
            Application.Current!.MainPage = new NewReminderPage();
        }
    }
}
