using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class AdminUsersPage : ContentPage
{
	public AdminUsersPage(string auth)
	{
		InitializeComponent();
        BindingContext = new AdminUsersViewModel(auth);
    }
}