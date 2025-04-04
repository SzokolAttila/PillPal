﻿using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Models;
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

        public void RemoveReminderCard(ReminderCardViewModel card)
        {
            ReminderCards.Remove(card);
        }

        public MainViewModel(int userId, string auth)
        {
            HomeButton = new HomeButtonViewModel(userId, auth);
            LocalNotificationCenter.Current.CancelAll();
            var reminders = handler.Get(userId, auth).OrderBy(x => x.When);
            TimeOnly now = TimeOnly.FromDateTime(DateTime.Now);
            //Put the reminders that are the most actual to the first place
            foreach (var reminder in reminders.Where(x=>x.When.CompareTo(now) >= 0))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = auth };
                cardModel.RemoveFromScreen = () => ReminderCards.Remove(cardModel);
                ReminderCards.Add(cardModel);
                _ = ReminderManager.CreateNotification(reminder, reminder.Medicine!);
            }

            //Then put after the reminders that are already passed
            foreach (var reminder in reminders.Where(x => x.When.CompareTo(now) < 0))
            {
                ReminderCardViewModel cardModel = new() { Reminder = reminder, Auth = auth };
                cardModel.RemoveFromScreen = () => ReminderCards.Remove(cardModel);
                ReminderCards.Add(cardModel);
                _ = ReminderManager.CreateNotification(reminder, reminder.Medicine!);
            }
        }
    }
}
