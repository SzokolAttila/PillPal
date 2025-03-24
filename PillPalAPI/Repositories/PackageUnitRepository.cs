using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class PackageUnitRepository : IItemStore<PackageUnit>
    {
        private readonly IDataStore _dataStore;
        public PackageUnitRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(PackageUnit item) => _dataStore.PackageUnits.Add(item);

        public bool Delete(int id) => _dataStore.PackageUnits.Remove(id);

        public PackageUnit? Get(int id) => _dataStore.PackageUnits[id];
        public IEnumerable<PackageUnit> GetAll() => _dataStore.PackageUnits.Values;
        public bool Update(PackageUnit item) => _dataStore.PackageUnits.Replace(item);
    }


}
