using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class PackageSizeRepository : IJoinStore<PackageSize>
    {
        private readonly IDataStore _dataStore;
        public PackageSizeRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(PackageSize item) => _dataStore.PackageSizes.Add(item);
        public bool Delete(int id) => _dataStore.PackageSizes.Remove(id);
        public IEnumerable<PackageSize> Get(int id) => _dataStore.PackageSizes.Values.Where(x => x.MedicineId == id);
        public IEnumerable<PackageSize> GetAll() => _dataStore.PackageSizes.Values;
        public bool Update(PackageSize item) => _dataStore.PackageSizes.Replace(item);
    }
}
