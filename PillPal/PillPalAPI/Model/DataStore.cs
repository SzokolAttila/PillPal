using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore : IDataStore
    {
        public Medicine Add(Medicine item)
        {
            throw new NotImplementedException();
        }

        public User Add(User item)
        {
            throw new NotImplementedException();
        }

        public Reminder Add(Reminder item)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Medicine Get(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Medicine> GetAll()
        {
            throw new NotImplementedException();
        }

        public Medicine Update(Medicine item)
        {
            throw new NotImplementedException();
        }

        public User Update(User item)
        {
            throw new NotImplementedException();
        }

        public Reminder Update(Reminder item)
        {
            throw new NotImplementedException();
        }

        User IItemStore<User>.Get(int id)
        {
            throw new NotImplementedException();
        }

        Reminder IItemStore<Reminder>.Get(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<User> IItemStore<User>.GetAll()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Reminder> IItemStore<Reminder>.GetAll()
        {
            throw new NotImplementedException();
        }
    }
}
