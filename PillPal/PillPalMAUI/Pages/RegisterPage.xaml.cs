using System.Text.RegularExpressions;

namespace PillPalMAUI.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
    }
	
    private async void ToLogin(object sender, TappedEventArgs e)
    {
		await Navigation.PushAsync(new LoginPage());
    }
}