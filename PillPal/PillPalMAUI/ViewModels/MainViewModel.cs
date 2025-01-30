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
    public class MainViewModel : BaseViewModel
    {
        ReminderAPIHandler handler;

        private ObservableCollection<ReminderCardViewModel> reminderCards;
        public ObservableCollection<ReminderCardViewModel> ReminderCards
        {
            get { return reminderCards; }
            set { reminderCards = value; Changed(); }
        }

        public MainViewModel(int userId, string auth)
        {
            handler = new ReminderAPIHandler();
            ReminderCards = new ObservableCollection<ReminderCardViewModel>();
            foreach (var reminder in handler.Get(userId, auth))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = auth };
                ReminderCards.Add(cardModel);
            }
        }
    }
}
