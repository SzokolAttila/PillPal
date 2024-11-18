using PillPalLib;
using PillPalLib.Interfaces;

namespace PillPalAPI.Model
{
    public interface IItemStore<T>  : IBaseStore<T> where T : IIdentified
    {
        T? Get(int id);
    }
}
