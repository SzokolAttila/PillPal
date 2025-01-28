using PillPalMAUI.ViewModels;

namespace PillPalMAUI;

public partial class ReminderCard : ContentView
{
	ReminderCardViewModel vm => (BindingContext as ReminderCardViewModel);
	public ReminderCard()
	{
		InitializeComponent();
		vm.Reminder = new PillPalLib.Reminder() { Medicine = new PillPalLib.Medicine("ACC", "gyógyszer", "gyártó", "400g"),TakingMethod = "Folyadékban elkeverve", When = TimeOnly.FromDateTime(DateTime.Now), DoseCount = 2, DoseMg = 100};
	}
}