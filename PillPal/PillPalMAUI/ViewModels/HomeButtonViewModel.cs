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
        }
        private string auth = string.Empty;
        private int userId;
        public string Auth
        {
            get => auth;
            set
            {
                auth = value;
                Changed();
            }
        }
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
                Changed();
            }
        }
        public ICommand NewReminderPressed { get; private set; }
        private void ToNewReminder()
        {
            Application.Current!.MainPage = new NewReminderPage(userId, auth);
        }
    }
}
