using PillPalLib;
using Plugin.LocalNotification;
using Plugin.LocalNotification.AndroidOption;
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
            var time = reminder.When.ToTimeSpan();
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
                    NotifyTime = reminder.When.CompareTo(TimeOnly.FromDateTime(DateTime.Now)) >= 0 ?
                        DateTime.Today.Add(time) : DateTime.Today.AddDays(1).Add(time),
                    RepeatType = NotificationRepeat.Daily,
                    Android = new()
                    {
                        AlarmType = AndroidAlarmType.RtcWakeup,
                    }
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
