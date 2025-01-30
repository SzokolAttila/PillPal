using System.Text.RegularExpressions;

namespace PillPalMAUI.Pages;

public partial class RegisterPage : ContentPage
{
	public RegisterPage()
	{
		InitializeComponent();
    }
	
    private void ToLogin(object sender, TappedEventArgs e)
    {
		Application.Current!.MainPage = new LoginPage();
    }
}