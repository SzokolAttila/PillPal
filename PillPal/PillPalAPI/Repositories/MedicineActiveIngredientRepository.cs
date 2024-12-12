using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class MedicineActiveIngredientRepository : IJoinStore<MedicineActiveIngredient>
    {
        private readonly IDataStore _dataStore;
        public MedicineActiveIngredientRepository(IDataStore dataStore)
        {
            this._dataStore = dataStore;
        }
        public bool Add(MedicineActiveIngredient item) => _dataStore.MedicineActiveIngredients.Add(item);
        public bool Delete(int id) => _dataStore.MedicineActiveIngredients.Remove(id);
        public IEnumerable<MedicineActiveIngredient> Get(int id) => _dataStore.MedicineActiveIngredients.Values.Where(x => x.MedicineId == id);
        public IEnumerable<MedicineActiveIngredient> GetAll() => _dataStore.MedicineActiveIngredients.Values;
        public bool Update(MedicineActiveIngredient item) => _dataStore.MedicineActiveIngredients.Replace(item);
    }
}
