using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class NewReminderPage : ContentPage
{
	public NewReminderPage(int userId, string auth)
	{
		InitializeComponent();
		BindingContext = new NewReminderViewModel() 
			{ Auth = auth, UserId = userId };
    }
}