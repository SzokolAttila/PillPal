using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Pages;

public partial class DetailPage : ContentPage
{
	public DetailPage(Reminder reminder, string auth)
	{
		InitializeComponent();
		BindingContext = new DetailPageViewModel(reminder.UserId, auth)
        {Medicine = reminder.Medicine! };
    }
}