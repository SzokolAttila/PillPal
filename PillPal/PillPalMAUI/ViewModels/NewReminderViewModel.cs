using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Pages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PillPalMAUI.ViewModels
{
    public class NewReminderViewModel : ViewModelBase
    {
        private readonly MedicineAPIHandler _medicineHandler;
        private readonly ReminderAPIHandler _reminderHandler;
        private string auth = string.Empty;
        public string Auth
        {
            get => auth;
            set
            {
                auth = value;
            }
        }
        private int userId;
        public int UserId
        {
            get => userId;
            set
            {
                userId = value;
            }
        }
        public NewReminderViewModel()
        {
            _reminderHandler = new();
            _medicineHandler = new();
            HomeButton = new HomeButtonViewModel()
                { UserId = userId, Auth = auth };
            Medicines = _medicineHandler.GetMedicines().OrderBy(x => x.Name);
            CreateReminder = new Command(CreateNewReminder);
        }

        private async void CreateNewReminder()
        {
            if (Medicine == null)
            {
                await Application.Current!.MainPage!.DisplayAlert("Hiba", "Nem választotta ki a gyógyszert!", "OK");
                return;
            }
            try
            {
                _reminderHandler.CreateReminder(new()
                {
                    MedicineId = Medicine.Id,
                    When = TimeOnly.FromTimeSpan(When).ToString(),
                    DoseCount = DoseCount,
                    TakingMethod = TakingMethod,
                    UserId = userId,

                }, auth);
                await Application.Current!.MainPage!.DisplayAlert("Sikeres létrehozás", "Sikeresen létrehozta az emlékeztetőt!", "Vissza a főoldalra");
                Application.Current!.MainPage = new MainPage(userId, auth);
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Hiba", $"Hiba történt az emlékeztető létrehozása közben: {ex.Message}", "OK");
            }
        }
        private HomeButtonViewModel homeButton;
        public HomeButtonViewModel HomeButton
        {
            get => homeButton;
            set
            {
                homeButton = value;
                Changed();
            }
        }
        private string takingMethod = string.Empty;
        public string TakingMethod
        {
            get => takingMethod;
            set
            {
                takingMethod = value;
                Changed();
            }
        }
        private int doseCount = 1;
        public int DoseCount
        {
            get => doseCount;
            set
            {
                doseCount = value;
                Changed();
            }
        }
        private string medicineName = string.Empty;
        public string MedicineName
        {
            get => medicineName;
            set
            {
                medicineName = value;
                if (medicineName == string.Empty)
                    Medicines = _medicineHandler.GetMedicines()
                        .OrderBy(x => x.Name);
                else
                {
                    Medicines = _medicineHandler.GetMedicines()
                    .Where(x => x.Name.Contains(medicineName, StringComparison.CurrentCultureIgnoreCase))
                    .OrderBy(x => x.Name);
                }
                Changed();
            }
        }
        private Medicine? medicine = null;
        public Medicine? Medicine
        {
            get => medicine;
            set
            {
                medicine = value;
                Changed();
            }
        }
        private IEnumerable<Medicine> medicines = [];
        public IEnumerable<Medicine> Medicines
        {
            get => medicines;
            set
            {
                medicines = value;
                Changed();
            }
        }
        private TimeSpan when;
        public TimeSpan When
        {
            get => when;
            set
            {
                when = value;
                Changed();
            }
        }
        public ICommand CreateReminder { get; private set; }
    }
}
