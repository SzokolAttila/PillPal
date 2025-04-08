using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalLib.DTOs.ReminderDTOs;
using PillPalMAUI.Models;
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
    public class EditReminderViewModel : ViewModelBase
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
            set 
            { 
                medName = value;
                Searched = new ObservableCollection<Medicine>(_allMedicines
                    .Where(x => x.Name.Contains(MedName, StringComparison.CurrentCultureIgnoreCase)));
                Changed(); 
            }
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
        private readonly IEnumerable<Medicine> _allMedicines;
        public EditReminderViewModel(Reminder reminder)
		{
            handler = new();
            medHandler = new();
            _allMedicines = medHandler.GetMedicines();
            Searched = new ObservableCollection<Medicine>(_allMedicines.OrderBy(x => x.Name));
            Reminder = reminder;
            When = new TimeSpan(Reminder.When.Hour, Reminder.When.Minute, 0);
            Modify = new Command(ModifyReminder);
            Cancel = new Command(StopEditing);
            var token = SecureStorage.Default.GetAsync("Token").Result;
            if (token == null)
            {
                SecureStorage.Default.Remove("UserId");
                SecureStorage.Default.Remove("Token");
                Application.Current!.MainPage!.DisplayAlert("Hiba", "Nincs bejelentkezve!", "OK");
                Application.Current!.MainPage = new LoginPage();
                return;
            }
            Auth = token;
        }
        private async void StopEditing()
        {
            // Check if user really wants to stop editing
            bool result = await Application.Current!.MainPage!.DisplayAlert("Változtatások eldobása", "Biztosan szeretnéd dobni a változtatásokat?", "Igen", "Nem");
            if (result)
            {
                Application.Current!.MainPage = new MainPage();
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
                await ReminderManager.UpdateNotification(reminder, reminder.Medicine!);
                Application.Current!.MainPage = new MainPage();
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Sikertelen módosítás!",
                    $"A módosítás nem sikerült. ({ex.Message})", "OK");
            }
        }

    }
}
