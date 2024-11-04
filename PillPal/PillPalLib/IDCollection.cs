using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class IDCollection<T> where T : IIdentified
    {
        private readonly IEnumerable<T> values;
        public List<T> Values => values.ToList();

        public IDCollection(IEnumerable<T> values)
        {
            this.values = values;
        }

        public T this[int id]
        {
            get
            {
                return Values.FirstOrDefault(x => x.Id == id) ?? throw new ArgumentException("Could not find element with this ID");
            }
        }
    }
}
