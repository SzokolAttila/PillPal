using PillPalLib;

namespace PillPalAPI.Model
{
    public interface IDataStore : IItemStore<Medicine>, IItemStore<User>, IItemStore<Reminder>
    {

    }
    public interface IItemStore<T>
    {
        IEnumerable<T> GetAll();
        bool Add(T item);
        T? Get(int id);
        bool Update(T item);
        bool Delete(int id);
    }
}
