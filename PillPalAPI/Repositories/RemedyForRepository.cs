using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Repositories
{
    public class RemedyForRepository : IItemStore<RemedyFor>
    {
        private readonly IDataStore _dataStore;
        public RemedyForRepository(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public bool Add(RemedyFor item) => _dataStore.RemedyForAilments.Add(item);
        public bool Delete(int id) => _dataStore.RemedyForAilments.Remove(id);
        public RemedyFor? Get(int id) => _dataStore.RemedyForAilments[id];
        public IEnumerable<RemedyFor> GetAll() => _dataStore.RemedyForAilments.Values;
        public bool Update(RemedyFor item) => _dataStore.RemedyForAilments.Replace(item);
    }
}
