using PillPalLib;
using Plugin.LocalNotification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalMAUI.Models
{
    public static class ReminderManager
    {
        public static async Task CreateNotification(Reminder reminder, Medicine medicine)
        {
            NotificationRequest notification = new()
            {
                NotificationId = reminder.Id,
                Title = "Szedd be a gyógyszered!",
                Description = $"Ne felejts el bevenni {reminder.DoseCount} " +
                    $"{medicine.PackageUnit!.Name} {medicine.Name}-t!" +
                    $"\nBevitel módja: {reminder.TakingMethod}",
                BadgeNumber = 1,
                CategoryType = NotificationCategoryType.Alarm,
                Schedule = new NotificationRequestSchedule
                {
                    NotifyTime = DateTime.Today.Add(reminder.When.ToTimeSpan()),
                    RepeatType = NotificationRepeat.Daily
                },
                Android = new()
                {
                    ChannelId = "reminder_channel"
                }
            };

            await LocalNotificationCenter.Current.Show(notification);
        }

        public static async Task UpdateNotification(Reminder reminder, Medicine medicine)
        {
            DeleteNotification(reminder.Id);
            await CreateNotification(reminder, medicine);
        }

        public static void DeleteNotification(int notificationId)
        {
           LocalNotificationCenter.Current.Cancel(notificationId);
        }
    }
}
