using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class ReminderRepository : IItemStore<Reminder>
    {
        private readonly IDataStore _dataStore;
        public ReminderRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(Reminder item) => _dataStore.Reminders.Add(item);

        public bool Delete(int id) => _dataStore.Reminders.Remove(id);

        public Reminder? Get(int id) => _dataStore.Reminders[id];

        public IEnumerable<Reminder> GetAll() => _dataStore.Reminders.Values;

        public bool Update(Reminder item) => _dataStore.Reminders.Replace(item);
    }
}
