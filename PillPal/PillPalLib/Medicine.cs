﻿using PillPalLib.Interfaces;
using System.Text.Json.Serialization;

namespace PillPalLib
{
    public class Medicine : IIdentified
    {
        [JsonConstructor]
        public Medicine(int id, string name, string description, int[] packageSizes, string manufacturer, string packageUnit)
        {
            Id = id;
            Name = name;
            Description = description;
            PackageSizes = packageSizes;
            Manufacturer = manufacturer;
            PackageUnit = packageUnit;
        }

        public Medicine(string name, string description, int[] packageSizes, string manufacturer, string packageUnit)
        {
            Name = name;
            Description = description;
            PackageSizes = packageSizes;
            Manufacturer = manufacturer;
            PackageUnit = packageUnit;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] PackageSizes { get; set; }
        public string Manufacturer { get; set; }
        public string PackageUnit { get; set; }
        [JsonIgnore]
        public List<Reminder> Reminders { get; set; } = [];
        [JsonIgnore]
        public List<MedicineSideEffect> SideEffects { get; set; } = [];
        [JsonIgnore]
        public List<MedicineActiveIngredient> ActiveIngredients { get; set; } = [];
        [JsonIgnore]
        public List<MedicineRemedyFor> MedicineRemedyForAilments { get; set; } = [];
    }
}
