using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Models;
using PillPalMAUI.Pages;
using PillPalMAUI.Resources.ContentViews;
using Plugin.LocalNotification;
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

        public void RemoveReminderCard(ReminderCardViewModel card)
        {
            ReminderCards.Remove(card);
        }

        public MainViewModel()
        {
            int id = Convert.ToInt32(SecureStorage.Default.GetAsync("UserId").Result);
            var token = SecureStorage.Default.GetAsync("Token").Result;
            if (string.IsNullOrEmpty(token))
            {
                SecureStorage.Default.Remove("UserId");
                SecureStorage.Default.Remove("Token");
                Application.Current!.MainPage!.DisplayAlert("Hiba", "Nincs bejelentkezve!", "OK");
                Application.Current!.MainPage = new LoginPage();
                return;
            }
            LocalNotificationCenter.Current.CancelAll();
            var reminders = handler.Get(id, token).OrderBy(x => x.When);
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            //Put the reminders that are the most actual to the first place
            foreach (var reminder in reminders.Where(x=>x.When.CompareTo(now) >= 0))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = token };
                cardModel.RemoveFromScreen = () => ReminderCards.Remove(cardModel);
                ReminderCards.Add(cardModel);
                _ = ReminderManager.CreateNotification(reminder, reminder.Medicine!);
            }

            //Then put after the reminders that are already passed
            foreach (var reminder in reminders.Where(x => x.When.CompareTo(now) < 0))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = token };
                cardModel.RemoveFromScreen = () => ReminderCards.Remove(cardModel);
                ReminderCards.Add(cardModel);
                _ = ReminderManager.CreateNotification(reminder, reminder.Medicine!);
            }
        }
    }
}
