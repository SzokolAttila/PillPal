using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class EditPage : ContentPage
{
	public EditPage(Reminder reminder, string auth)
	{
		InitializeComponent();
		BindingContext = new EditViewModel(reminder, auth);
    }
}