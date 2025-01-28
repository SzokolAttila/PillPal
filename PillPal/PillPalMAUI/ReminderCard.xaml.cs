using PillPalMAUI.ViewModels;

namespace PillPalMAUI;

public partial class ReminderCard : ContentView
{
	ReminderCardViewModel vm => (BindingContext as ReminderCardViewModel);
	public ReminderCard()
	{
		InitializeComponent();
		vm.Reminder = new PillPalLib.Reminder() { Medicine = new PillPalLib.Medicine("ACC", "gy�gyszer", "gy�rt�", "400g"),TakingMethod = "Folyad�kban elkeverve", When = TimeOnly.FromDateTime(DateTime.Now), DoseCount = 2, DoseMg = 100};
	}
}