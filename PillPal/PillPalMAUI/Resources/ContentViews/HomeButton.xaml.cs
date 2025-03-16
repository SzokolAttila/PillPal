using PillPalMAUI.Pages;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Resources.ContentViews;

public partial class HomeButton : ContentView
{
	private bool _isOpen = false;
    public HomeButton()
	{
		InitializeComponent();
	}

    private async Task OpenMenu()
    {
        await Task.WhenAll(
            NewReminder.FadeTo(1, 200, Easing.CubicInOut),
            NewReminder.TranslateTo(-80, -80, 250, Easing.SinOut),
            Home.FadeTo(1, 200, Easing.CubicInOut),
            Home.TranslateTo(0, -120, 250, Easing.SinOut),
            Settings.FadeTo(1, 200, Easing.CubicInOut),
            Settings.TranslateTo(80, -80, 250, Easing.SinOut)
        );
    }

    private async Task CloseMenu()
    {
        await Task.WhenAll(
            NewReminder.FadeTo(0, 200, Easing.CubicInOut),
            NewReminder.TranslateTo(0, 0, 250, Easing.SinOut),
            Home.FadeTo(0, 200, Easing.CubicInOut),
            Home.TranslateTo(0, 120, 250, Easing.SinOut),
            Settings.FadeTo(0, 200, Easing.CubicInOut),
            Settings.TranslateTo(0, 0, 250, Easing.SinOut)
        );
    }

    private async void Menu_Clicked(object sender, EventArgs e)
    {
        if (_isOpen)
        {
            await CloseMenu();
            await MenuButton.RotateTo(0, 250, Easing.CubicInOut);
        }
        else
        {
            await MenuButton.RotateTo(90, 250, Easing.CubicInOut);
            await OpenMenu();
        }
        _isOpen = !_isOpen;
    }
}