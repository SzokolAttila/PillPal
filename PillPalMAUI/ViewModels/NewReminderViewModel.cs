using PillPalLib;
using PillPalLib.APIHandlers;
using PillPalMAUI.Models;
using PillPalMAUI.Pages;
using Plugin.LocalNotification;
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
        private readonly MedicineAPIHandler _medicineHandler = new();
        private readonly ReminderAPIHandler _reminderHandler = new();  
        public string Auth { get; set; } = string.Empty;
        public int UserId { get; set; }
        public NewReminderViewModel()
        {
            Medicines = _medicineHandler.GetMedicines().OrderBy(x => x.Name);
            CreateReminder = new Command(CreateNewReminder);
            if (SecureStorage.Default.GetAsync("Token").Result == null)
            {
                SecureStorage.Default.Remove("UserId");
                SecureStorage.Default.Remove("Token");
                Application.Current!.MainPage!.DisplayAlert("Hiba", "Nincs bejelentkezve!", "OK");
                Application.Current!.MainPage = new LoginPage();
                return;
            }
            UserId = Convert.ToInt32(SecureStorage.Default.GetAsync("UserId").Result);
            Auth = SecureStorage.Default.GetAsync("Token").Result!;
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
                var created = _reminderHandler.CreateReminder(new()
                {
                    MedicineId = Medicine.Id,
                    When = TimeOnly.FromTimeSpan(When).ToString(),
                    DoseCount = DoseCount,
                    TakingMethod = TakingMethod,
                    UserId = UserId,

                }, Auth);
                
                if (created != null)
                {
                    await ReminderManager.CreateNotification(created, Medicine!);
                    await Application.Current!.MainPage!.DisplayAlert("Sikeres létrehozás", "Sikeresen létrehozta az emlékeztetőt!", "Vissza a főoldalra");
                    Application.Current!.MainPage = new MainPage();
                }
                else
                {
                    await Application.Current!.MainPage!.DisplayAlert("Hiba", "Hiba történt az emlékeztető létrehozása közben.", "OK");
                }
            }
            catch (Exception ex)
            {
                await Application.Current!.MainPage!.DisplayAlert("Hiba", $"Hiba történt az emlékeztető létrehozása közben: {ex.Message}", "OK");
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
