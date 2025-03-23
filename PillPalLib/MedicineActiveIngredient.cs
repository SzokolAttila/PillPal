using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class MedicineActiveIngredient : IIdentified
    {
        public int Id { get; set; }
        public int MedicineId {  get; set; }
        public Medicine? Medicine { get; set; }
        public int ActiveIngredientId { get; set; }
        public ActiveIngredient? ActiveIngredient { get; set; }
    }
}
