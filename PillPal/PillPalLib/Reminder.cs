﻿using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class Reminder : IIdentified
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int MedicineId { get; set; }
        public TimeOnly When { get; set; }
        public double DoseCount { get; set; }
        public int DoseMg {  get; set; }
        public string TakingMethod { get; set; } = "";
    }
}
