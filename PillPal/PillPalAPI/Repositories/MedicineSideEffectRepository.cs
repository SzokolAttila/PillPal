using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class MedicineSideEffectRepository : IJoinStore<MedicineSideEffect>
    {
        private readonly IDataStore _dataStore;
        public MedicineSideEffectRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public bool Add(MedicineSideEffect item) => _dataStore.MedicineSideEffects.Add(item);
        public bool Delete(int id) => _dataStore.MedicineSideEffects.Remove(id);
        public IEnumerable<MedicineSideEffect> Get(int id) => _dataStore.MedicineSideEffects.Values.Where(x => x.MedicineId == id);
        public IEnumerable<MedicineSideEffect> GetAll() => _dataStore.MedicineSideEffects.Values;
        public bool Update(MedicineSideEffect item) => _dataStore.MedicineSideEffects.Replace(item);
    }
}
