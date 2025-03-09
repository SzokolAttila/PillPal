using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.ReminderDTOs;
using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class EditViewModel : ViewModelBase
    {
		private Reminder reminder;

		public Reminder Reminder
		{
			get { return reminder; }
			set {
                reminder = value;
                Changed();
            }
		}

        public ICommand SearchMeds => new Command(() =>
        {
            Searched = new ObservableCollection<Medicine>(medHandler.GetMedicines()
                .Where(x=>x.Name.ToLower().Contains(MedName.ToLower())));
        });

        private ObservableCollection<Medicine> searched;

        public ObservableCollection<Medicine> Searched
        {
            get { return searched; }
            set { searched = value; Changed(); }
        }

        private string medName;

        public string MedName
        {
            get { return medName; }
            set { medName = value; Changed(); }
        }


        private Medicine? selected;

        public Medicine? Selected
        {
            get { return selected; }
            set { 
                selected = value;
                if (selected != null)
                {
                    Reminder r = Reminder;
                    r.MedicineId = selected.Id;
                    r.Medicine = selected;
                    Reminder = r;
                }
                Changed();
            }
        }

        private TimeSpan when;

        public TimeSpan When
        {
            get { return when; }
            set { 
                when = value;
                Reminder.When = TimeOnly.FromTimeSpan(when);
                Changed();
            }
        }


        public ICommand Modify { get; private set; }
        public ICommand Cancel { get; private set; }
        private readonly ReminderAPIHandler handler;
        private readonly MedicineAPIHandler medHandler;
        private readonly string Auth;

        public EditViewModel(Reminder reminder, string auth)
		{
            handler = new();
            medHandler = new();
            Auth = auth;
            Reminder = reminder;
            When = new TimeSpan(Reminder.When.Hour, Reminder.When.Minute, 0);
            Modify = new Command(ModifyReminder);
            Cancel = new Command(StopEditing);
        }

        private async void StopEditing()
        {
            // Check if user really wants to stop editing
            bool result = await Application.Current!.MainPage!.DisplayAlert("Változtatások eldobása", "Biztosan szeretnéd dobni a változtatásokat?", "Igen", "Nem");
            if (result)
            {
                Application.Current!.MainPage = new MainPage(reminder.UserId, Auth);
            }
        }
        private async void ModifyReminder()
        {
            try
            {
                CreateReminderDto dto = new CreateReminderDto() {
                    UserId = reminder.UserId,
                    MedicineId = reminder.MedicineId,
                    TakingMethod = reminder.TakingMethod,
                    DoseCount = reminder.DoseCount,
                    When = reminder.When.ToString()
                };
                handler.EditReminder(reminder.Id, dto, Auth);
                await Application.Current!.MainPage!.DisplayAlert("Sikeres módosítás!",
                    $"A módosítás sikeres volt.", "OK");
                Application.Current!.MainPage = new MainPage(reminder.UserId, Auth);
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Sikertelen módosítás!",
                    $"A módosítás nem sikerült. ({ex.Message})", "OK");
            }
        }

    }
}
