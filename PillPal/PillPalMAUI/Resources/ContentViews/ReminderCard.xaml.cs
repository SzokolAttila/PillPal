using PillPalLib;
using PillPalMAUI.ViewModels;

namespace PillPalMAUI.Resources.ContentViews;

public partial class ReminderCard : ContentView
{
	public ReminderCardViewModel vm => (BindingContext as ReminderCardViewModel);
	public ReminderCard()
	{
		InitializeComponent();
    }
}