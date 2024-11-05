using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore : IDataStore
    {
        private readonly IDCollection<User> _users;
        private readonly IDCollection<Medicine> _medicines;
        private readonly IDCollection<Reminder> _reminders;
        public DataStore(IEnumerable<User> users, IEnumerable<Medicine> medicines, IEnumerable<Reminder> reminders)
        {
            _users = new IDCollection<User>(users);
            _medicines = new IDCollection<Medicine> (medicines);
            _reminders = new IDCollection<Reminder> (reminders);
        }
        public bool Add(Medicine item) => _medicines.Add(item);

        public bool Delete(int id) => _medicines.Remove(id);
        public bool Update(Medicine item) => _medicines.Replace(item);
        public IEnumerable<Medicine> GetAll() => _medicines.Values;
        public Medicine? Get(int id) => _medicines[id];

        public bool Add(User item)
        {
            throw new NotImplementedException();
        }
        bool IItemStore<User>.Delete(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(User item)
        {
            throw new NotImplementedException();
        }
        IEnumerable<User> IItemStore<User>.GetAll()
        {
            return _users.Values;
        }
        User? IItemStore<User>.Get(int id)
        {
            throw new NotImplementedException();
        }

        public bool Add(Reminder item)
        {
            throw new NotImplementedException();
        }
        bool IItemStore<Reminder>.Delete(int id)
        {
            throw new NotImplementedException();
        }
        public bool Update(Reminder item)
        {
            throw new NotImplementedException();
        }
        IEnumerable<Reminder> IItemStore<Reminder>.GetAll()
        {
            return _reminders.Values;
        }
        Reminder? IItemStore<Reminder>.Get(int id)
        {
            throw new NotImplementedException();
        }
    }
}
