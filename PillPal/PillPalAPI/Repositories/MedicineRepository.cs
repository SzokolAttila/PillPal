using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class MedicineRepository : IItemStore<Medicine>
    {
        private readonly DataStore _dataStore;
        public MedicineRepository(DataStore dataStore) { 
            _dataStore = dataStore;
        }
        public bool Add(Medicine item) => _dataStore._medicines.Add(item);
        public bool Delete(int id) => _dataStore._medicines.Remove(id);
        public bool Update(Medicine item) => _dataStore._medicines.Replace(item);
        public IEnumerable<Medicine> GetAll() => _dataStore._medicines.Values;
        public Medicine? Get(int id) => _dataStore._medicines[id];
    }
}
