using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Resources.ContentViews;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PillPalMAUI.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        readonly ReminderAPIHandler handler = new();

        private ObservableCollection<ReminderCardViewModel> reminderCards = new();
        public ObservableCollection<ReminderCardViewModel> ReminderCards
        {
            get { return reminderCards; }
            set { reminderCards = value; Changed(); }
        }
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
        public MainViewModel(int userId, string auth)
        {
            HomeButton = new HomeButtonViewModel(userId, auth);
            foreach (var reminder in handler.Get(userId, auth))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = auth };
                ReminderCards.Add(cardModel);
            }
        }
    }
}
