using PillPalLib.DTOs.MedicineDTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.Mappers
{
    public static class MedicineMapper
    {
        public static MedicineDto ToMedicineDto(this Medicine Medicine)
        {
            return new MedicineDto()
            {
                Id = Medicine.Id,
                Name = Medicine.Name,
                Description = Medicine.Description,
                Manufacturer = Medicine.Manufacturer,
                PackageUnit = Medicine.PackageUnit,
                ActiveIngredients = Medicine.ActiveIngredients.Select(x => x.Ingredient).ToList(),
                PackageSizes = Medicine.PackageSizes.Select(x => x.Size).ToList(),
                RemedyForAilments = Medicine.RemedyForAilments.Select(x => x.Ailment).ToList(),
                SideEffects = Medicine.SideEffects.Select(x => x.Effect).ToList()
            };
        }
    }
}
