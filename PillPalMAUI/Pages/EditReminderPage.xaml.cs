using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class EditReminderPage : ContentPage
{
	public EditReminderPage(Reminder reminder)
	{
		InitializeComponent();
		BindingContext = new EditReminderViewModel(reminder);
    }
}