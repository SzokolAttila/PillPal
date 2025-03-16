using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class EditReminderPage : ContentPage
{
	public EditReminderPage(Reminder reminder, string auth)
	{
		InitializeComponent();
		BindingContext = new EditReminderViewModel(reminder, auth);
    }
}