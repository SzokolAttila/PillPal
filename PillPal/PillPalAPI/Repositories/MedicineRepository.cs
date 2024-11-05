using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class MedicineRepository : IItemStore<Medicine>
    {
        private readonly IDataStore _dataStore;
        public MedicineRepository(IDataStore dataStore) { 
            _dataStore = dataStore;
        }
        public bool Add(Medicine item) => _dataStore.Medicines.Add(item);
        public bool Delete(int id) => _dataStore.Medicines.Remove(id);
        public bool Update(Medicine item) => _dataStore.Medicines.Replace(item);
        public IEnumerable<Medicine> GetAll() => _dataStore.Medicines.Values;
        public Medicine? Get(int id) => _dataStore.Medicines[id];
    }
}
