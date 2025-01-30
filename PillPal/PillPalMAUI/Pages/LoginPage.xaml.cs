namespace PillPalMAUI.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void ToRegistration (object sender, TappedEventArgs e)
    {
		await Navigation.PushAsync(new RegisterPage());
    }
}