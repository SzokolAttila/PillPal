namespace PillPalMAUI.Pages;

public partial class LoginPage : ContentPage
{
	public LoginPage()
	{
		InitializeComponent();
	}

    private void ToRegistration (object sender, TappedEventArgs e)
    {
		Application.Current!.MainPage = new RegisterPage();
    }
}