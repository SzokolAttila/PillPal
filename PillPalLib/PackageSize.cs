using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class PackageSize : IIdentified
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public int Size { get; set; }
    }
}
