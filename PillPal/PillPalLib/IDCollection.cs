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
        private IEnumerable<T> values;
        public IEnumerable<T> Values => values.OrderBy(x => x.Id).Select(x => x);

        public IDCollection(IEnumerable<T> values)
        {
            this.values = values;
        }
        public int Size => values.Count();
        public bool Remove(int id)
        {
            if (!values.Any(x => x.Id == id))
                return false;
            values = values.Where(x => x.Id != id);
            return true;
        }
        private int getFreeID() {
            int i = 1;
            while (this[i] != null) { i++; } // fills the ID gaps this way :D
            return i;
        }
        public bool Add(T item)
        {
            if (values.Any(x => x.Id == item.Id))
                return false;
            item.Id = getFreeID();
            values = values.Append(item);
            return true;
        }
        public bool Replace(T item)
        {
            if (!values.Any(x => x.Id == item.Id))
                return false;
            values = values.Where(x => x.Id != item.Id);
            values = values.Append(item);
            return true;
        }
        public T? this[int id]
        {
            get => Values.FirstOrDefault(x => x.Id == id);
        }
    }
}
