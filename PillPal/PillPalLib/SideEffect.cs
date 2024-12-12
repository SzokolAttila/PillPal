using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class SideEffect : IIdentified
    {
        public int Id { get; set; }
        public string Effect {  get; set; } = string.Empty;
        [JsonIgnore]
        public List<MedicineSideEffect> MedicineSideEffects { get; set; } = [];
    }
}
