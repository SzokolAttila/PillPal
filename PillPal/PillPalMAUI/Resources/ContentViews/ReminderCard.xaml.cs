using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Resources.ContentViews;

public partial class ReminderCard : ContentView
{
	public ReminderCardViewModel vm => (BindingContext as ReminderCardViewModel);
	public ReminderCard()
	{
		InitializeComponent();
        vm.Remove = new Command(Remove_Clicked);
        vm.Info = new Command(Info_Clicked);
        vm.Edit = new Command(Edit_Clicked);
    }

    private void Remove_Clicked()
    {
        // Check if user really wants to remove the reminder
        // Remove the reminder from the database
        vm.IsVisible = false; // Remove the reminder from the view
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