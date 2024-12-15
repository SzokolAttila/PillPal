using PillPalLib;

namespace PillPalAPI.Model
{
    public interface IDataStore
    {
        public IDCollection<User> Users { get; }
        public IDCollection<Medicine> Medicines { get; }
        public IDCollection<Reminder> Reminders { get; }
        public IDCollection<SideEffect> SideEffects { get; }
        public IDCollection<MedicineSideEffect> MedicineSideEffects { get; }
        public IDCollection<ActiveIngredient> ActiveIngredients { get; }
        public IDCollection<MedicineActiveIngredient> MedicineActiveIngredients { get; }
        public IDCollection<MedicineRemedyFor> MedicineRemedyForAilments { get; }
        public IDCollection<RemedyFor> RemedyForAilments { get; }
        public IDCollection<PackageSize> PackageSizes { get; }
    }
}
