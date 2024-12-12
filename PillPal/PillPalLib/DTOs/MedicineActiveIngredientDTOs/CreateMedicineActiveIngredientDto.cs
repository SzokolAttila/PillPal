using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.DTOs.MedicineActiveIngredientDTOs
{
    public class CreateMedicineActiveIngredientDto
    {
        public int ActiveIngredientId {  get; set; }
        public int MedicineId { get; set; }
    }
}
