using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore : IDataStore { 
        public IDCollection<User> Users { get; }
        public IDCollection<Medicine> Medicines { get; }
        public IDCollection<Reminder> Reminders { get; }
        public DataStore(IEnumerable<User> users, IEnumerable<Medicine> medicines, IEnumerable<Reminder> reminders)
        {
            Users = new IDCollection<User>(users);
            Medicines = new IDCollection<Medicine> (medicines);
            Reminders = new IDCollection<Reminder> (reminders);
        }
    }
}
