using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.DTOs.MedicineDTOs
{
    public class MedicineDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Manufacturer { get; set; } = string.Empty;
        public string PackageUnit { get; set; } = string.Empty;
        public List<int> PackageSizes { get; set; } = [];
        public List<string> SideEffects { get; set; } = [];
        public List<string> RemedyForAilments { get; set; } = [];  
        public List<string> ActiveIngredients { get; set; } = [];
    }
}
