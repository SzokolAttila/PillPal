using PillPalLib.Interfaces;

namespace PillPalAPI.Model
{
    public interface IJoinStore<T> where T : IIdentified
    {
        IEnumerable<T> GetAll();
        bool Add(T item);
        IEnumerable<T> Get(int id);
        bool Update(T item);
        bool Delete(int id);
    }
}
