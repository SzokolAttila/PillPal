using PillPalLib;

namespace PillPalAPI.Model
{
    public interface IDataStore
    {
        public IDCollection<User> Users { get; }
        public IDCollection<Medicine> Medicines { get; }
        public IDCollection<Reminder> Reminders { get; }
    }
}
