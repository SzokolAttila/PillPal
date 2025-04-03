using Android.App;
using Android.Content.PM;
using Android.OS;

namespace PillPalMAUI
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        protected override void OnCreate(Bundle? savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            if (Build.VERSION.SdkInt >= BuildVersionCodes.O && OperatingSystem.IsAndroidVersionAtLeast(26))
            {
                var channel = new NotificationChannel("reminder_channel", "Reminders", NotificationImportance.High);
                channel.Description = "Channel for reminder notifications";
                
                ((NotificationManager)GetSystemService(NotificationService)!)
                    .CreateNotificationChannel(channel);
            }
        }
    }
}
