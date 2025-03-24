using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class SideEffectRepository : IItemStore<SideEffect>
    {
        private readonly IDataStore _dataStore;
        public SideEffectRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(SideEffect item) => _dataStore.SideEffects.Add(item);
        public bool Delete(int id) => _dataStore.SideEffects.Remove(id);
        public SideEffect? Get(int id) => _dataStore.SideEffects[id];
        public IEnumerable<SideEffect> GetAll() => _dataStore.SideEffects.Values;
        public bool Update(SideEffect item) => _dataStore.SideEffects.Replace(item);
    }
}
