namespace PillPalMAUI.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private async void ToRegistrationPage(object sender, TappedEventArgs e)
    {
        await Navigation.PopAsync();
        await Navigation.PushAsync(new RegisterPage());
    }
}