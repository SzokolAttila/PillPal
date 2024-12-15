using PillPalLib.Interfaces;
using System.Text.Json.Serialization;

namespace PillPalLib
{
    public class Medicine : IIdentified
    {
        [JsonConstructor]
        public Medicine(int id, string name, string description, string manufacturer, string packageUnit)
        {
            Id = id;
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
            PackageUnit = packageUnit;
        }

        public Medicine(string name, string description, string manufacturer, string packageUnit)
        {
            Name = name;
            Description = description;
            Manufacturer = manufacturer;
            PackageUnit = packageUnit;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Manufacturer { get; set; }
        public string PackageUnit { get; set; }
        public List<PackageSize> PackageSizes { get; set; } = [];
        public List<SideEffect> SideEffects { get; set; } = [];
        public List<ActiveIngredient> ActiveIngredients { get; set; } = [];
        public List<RemedyFor> RemedyForAilments { get; set; } = [];
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
