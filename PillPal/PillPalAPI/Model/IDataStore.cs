using PillPalLib;

namespace PillPalAPI.Model
{
    public interface IDataStore : IItemStore<Medicine>, IItemStore<User>, IItemStore<Reminder>
    {

    }
    public interface IItemStore<T>
    {
        IEnumerable<T> GetAll();
        T Get(int id);
        T Add(T item);
        T Update(T item);
        void Delete(int id);
    }
}
