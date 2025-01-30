using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Resources.ContentViews;

public partial class ReminderCard : ContentView
{
	public ReminderCardViewModel vm => (BindingContext as ReminderCardViewModel);
	public ReminderCard()
	{
		InitializeComponent();
        vm.Info = new Command(Info_Clicked);
        vm.Edit = new Command(Edit_Clicked);
    }

    private void Info_Clicked()
    {
        // Show the information for the reminder
    }

    private void Edit_Clicked()
    {
        // Edit the reminder
    }
}