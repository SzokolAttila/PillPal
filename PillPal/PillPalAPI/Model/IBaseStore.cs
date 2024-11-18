using PillPalLib.Interfaces;

namespace PillPalAPI.Model
{
    public interface IBaseStore <T> where T : IIdentified
    {
        IEnumerable<T> GetAll();
        bool Add(T item);
        bool Update(T item);
        bool Delete(int id);
    }
}
