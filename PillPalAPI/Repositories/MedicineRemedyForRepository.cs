using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class MedicineRemedyForRepository : IJoinStore<MedicineRemedyFor>
    {
        private readonly IDataStore _dataStore;
        public MedicineRemedyForRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(MedicineRemedyFor item) => _dataStore.MedicineRemedyForAilments.Add(item);
        public bool Delete(int id) => _dataStore.MedicineRemedyForAilments.Remove(id);
        public IEnumerable<MedicineRemedyFor> Get(int id) => _dataStore.MedicineRemedyForAilments.Values.Where(x => x.MedicineId == id);
        public IEnumerable<MedicineRemedyFor> GetAll() => _dataStore.MedicineRemedyForAilments.Values;
        public bool Update(MedicineRemedyFor item) => _dataStore.MedicineRemedyForAilments.Replace(item);
    }
}
