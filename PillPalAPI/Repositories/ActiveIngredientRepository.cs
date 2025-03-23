using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class ActiveIngredientRepository : IItemStore<ActiveIngredient>
    {
        private readonly IDataStore _dataStore;
        public ActiveIngredientRepository(IDataStore dataStore) 
        {
            _dataStore = dataStore; 
        }
        public bool Add(ActiveIngredient item) => _dataStore.ActiveIngredients.Add(item);
        public bool Delete(int id) => _dataStore.ActiveIngredients.Remove(id);
        public ActiveIngredient? Get(int id) => _dataStore.ActiveIngredients[id];
        public IEnumerable<ActiveIngredient> GetAll() => _dataStore.ActiveIngredients.Values;
        public bool Update(ActiveIngredient item) => _dataStore.ActiveIngredients.Replace(item);
    }
}
