using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class ReminderRepository : IJoinStore<Reminder>
    {
        private readonly IDataStore _dataStore;
        public ReminderRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(Reminder item) => _dataStore.Reminders.Add(item);

        public bool Delete(int id) => _dataStore.Reminders.Remove(id);

        public IEnumerable<Reminder> Get(int id) => _dataStore.Reminders.Values.Where(x => x.UserId == id);
        public IEnumerable<Reminder> GetAll() => _dataStore.Reminders.Values;

        public bool Update(Reminder item) => _dataStore.Reminders.Replace(item);
    }
}
