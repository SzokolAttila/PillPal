using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib.DTOs.MedicineSideEffectDTOs
{
    public class CreateMedicineSideEffectDto
    {
        public int MedicineId { get; set; }
        public int SideEffectId { get; set; }
    }
}
