using PillPalLib;

namespace PillPalAPI.Model
{
    public interface IItemStore<T>
    {
        IEnumerable<T> GetAll();
        bool Add(T item);
        T? Get(int id);
        bool Update(T item);
        bool Delete(int id);
    }
}
