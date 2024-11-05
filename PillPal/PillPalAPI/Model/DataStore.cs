using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore { 
        public readonly IDCollection<User> _users;
        public readonly IDCollection<Medicine> _medicines;
        public readonly IDCollection<Reminder> _reminders;
        public DataStore(IEnumerable<User> users, IEnumerable<Medicine> medicines, IEnumerable<Reminder> reminders)
        {
            _users = new IDCollection<User>(users);
            _medicines = new IDCollection<Medicine> (medicines);
            _reminders = new IDCollection<Reminder> (reminders);
        }
    }
}
