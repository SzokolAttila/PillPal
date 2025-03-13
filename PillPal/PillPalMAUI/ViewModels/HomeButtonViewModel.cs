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
        public string Auth { get; set; } = string.Empty;
        public int UserId { get; set; }
        public ICommand NewReminderPressed { get; private set; }
        private void ToNewReminder()
        {
            Application.Current!.MainPage = new NewReminderPage(UserId, Auth);
        }
    }
}
