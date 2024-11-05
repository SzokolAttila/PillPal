using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class UserRepository : IItemStore<User>
    {
        private readonly IDataStore _dataStore;
        public UserRepository(IDataStore dataStore) { 
            _dataStore = dataStore;
        }
        public bool Add(User item) => _dataStore.Users.Add(item);
        public bool Delete(int id) => _dataStore.Users.Remove(id);
        public User? Get(int id) => _dataStore.Users[id];
        public IEnumerable<User> GetAll() => _dataStore.Users.Values;
        public bool Update(User item) => _dataStore.Users.Replace(item);
    }
}
