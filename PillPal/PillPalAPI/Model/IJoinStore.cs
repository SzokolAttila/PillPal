using PillPalLib.Interfaces;

namespace PillPalAPI.Model
{
    public interface IJoinStore<T> : IBaseStore<T> where T : IIdentified
    {
        IEnumerable<T> Get(int id);
    }
}
