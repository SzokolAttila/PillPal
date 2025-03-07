using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Pages;

namespace PillPalMAUI.ViewModels
{
    public class ReminderCardViewModel : ViewModelBase
    {
        private Reminder _reminder;

        public Reminder Reminder
        {
            get { return _reminder; }
            set { _reminder = value; Changed(); }
        }

        private bool isVisible;

        public bool IsVisible
        {
            get { return isVisible; }
            set { isVisible = value; Changed(); }
        }

        public ICommand Remove { get; set; }
        public ICommand Info { get; set; }
        public ICommand Edit { get; set; }
        private ReminderAPIHandler _reminderAPIHandler;
        public string Auth { get; set; }

        public ReminderCardViewModel()
        {
            _reminderAPIHandler = new ReminderAPIHandler();
            IsVisible = true;
            Remove = new Command(Remove_Clicked);
            Info = new Command(Info_Clicked);
            Edit = new Command(Edit_Clicked);
        }

        private async void Remove_Clicked()
        {
            // Check if user really wants to remove the reminder
            bool result = await Application.Current!.MainPage!.DisplayAlert("Emlékeztető törlése", "Biztosan szeretnéd törölni az emlékeztetőt?", "Igen", "Nem");
            if (result)
            {
                _reminderAPIHandler.DeleteReminder(Reminder.Id, Auth); // Remove the reminder from the database
                IsVisible = false; // Remove the reminder from the view
            }
        }

        private async void Info_Clicked()
        {

        }

        private async void Edit_Clicked()
        {
            Application.Current!.MainPage = new EditPage(Reminder, Auth);
        }
    }
}
