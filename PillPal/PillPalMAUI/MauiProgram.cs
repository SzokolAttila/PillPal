using CommunityToolkit.Maui;
using Microsoft.Extensions.Logging;

namespace PillPalMAUI
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.ConfigureMauiHandlers(handlers =>
            {
                #if ANDROID
                AppDomain.CurrentDomain.SetData("android:usesCleartextTraffic", true);
                #endif
            });
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                    fonts.AddFont("PlayfairDisplay-VariableFont_wght.ttf", "PlayfairDisplay");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
