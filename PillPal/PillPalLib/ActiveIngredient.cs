using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class ActiveIngredient : IIdentified
    {
        public int Id { get; set; }
        public string Ingredient { get; set; } = string.Empty;
        [JsonIgnore]
        public List<MedicineActiveIngredient> MedicineActiveIngredients { get; set; } = [];
        [JsonIgnore]
        public List<Medicine> Medicines { get; set; } = [];
    }
}
