using PillPalLib.Interfaces;
using System.Text.Json.Serialization;

namespace PillPalLib
{
    public class Medicine : IIdentified
    {
        [JsonConstructor]
        public Medicine(int id, string name, string description, string manufacturer, int packageUnitId)
        {
            Id = id;
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
            PackageUnitId = packageUnitId;
        }

        public Medicine(string name, string description, string manufacturer, int packageUnitId)
        {
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
            PackageUnitId = packageUnitId;
        }

        public Medicine() { }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public int PackageUnitId { get; set; }
        public PackageUnit? PackageUnit { get; set; }
        [JsonIgnore]
        public IEnumerable<string> SideEffects => SideEffectObjects.Select(x => x.Effect);
        [JsonIgnore]
        public IEnumerable<int> PackageSizes => PackageSizeObjects.Select(x => x.Size);
        [JsonIgnore]
        public IEnumerable<string> ActiveIngredients => ActiveIngredientObjects.Select(x => x.Ingredient);
        [JsonIgnore]
        public IEnumerable<string> RemedyForAilments => RemedyForObjects.Select(x => x.Ailment);
        public List<PackageSize> PackageSizeObjects { get; set; } = [];
        public List<SideEffect> SideEffectObjects { get; set; } = [];
        public List<ActiveIngredient> ActiveIngredientObjects { get; set; } = [];
        public List<RemedyFor> RemedyForObjects { get; set; } = [];
        [JsonIgnore]
        public List<Reminder> Reminders { get; set; } = [];
        [JsonIgnore]
        public List<MedicineSideEffect> MedicineSideEffects { get; set; } = [];
        [JsonIgnore]
        public List<MedicineActiveIngredient> MedicineActiveIngredients { get; set; } = [];
        [JsonIgnore]
        public List<MedicineRemedyFor> MedicineRemedyForAilments { get; set; } = [];
    }
}
