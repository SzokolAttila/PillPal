using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class SettingsPage : ContentPage
{
	public SettingsPage(string auth, int userId)
	{
		InitializeComponent();
        BindingContext = new SettingsViewModel(userId, auth);
    }
}