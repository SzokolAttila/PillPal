using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class MedicineSideEffect : IIdentified
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public Medicine? Medicine { get; set; }
        public int SideEffectId { get; set; }
        public SideEffect? SideEffect { get; set; }
    }
}
