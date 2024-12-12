﻿using PillPalLib.Interfaces;
using System.Text.Json.Serialization;

namespace PillPalLib
{
    public class Medicine : IIdentified
    {
        [JsonConstructor]
        public Medicine(int id, string name, string description, int[] packageSizes, string manufacturer, string[] remedyFor, string packageUnit)
        {
            Id = id;
            Name = name;
            Description = description;
            PackageSizes = packageSizes;
            Manufacturer = manufacturer;
            RemedyFor = remedyFor;
            PackageUnit = packageUnit;
        }

        public Medicine(string name, string description, int[] packageSizes, string manufacturer, string[] remedyFor, string packageUnit)
        {
            Name = name;
            Description = description;
            PackageSizes = packageSizes;
            Manufacturer = manufacturer;
            RemedyFor = remedyFor;
            PackageUnit = packageUnit;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] PackageSizes { get; set; }
        public string Manufacturer { get; set; }
        public string[] RemedyFor {  get; set; }
        public string PackageUnit { get; set; }
        [JsonIgnore]
        public List<Reminder> Reminders { get; set; } = [];
        [JsonIgnore]
        public List<MedicineSideEffect> SideEffects { get; set; } = [];
        [JsonIgnore]
        public List<MedicineActiveIngredient> ActiveIngredients { get; set; } = [];
    }
}
