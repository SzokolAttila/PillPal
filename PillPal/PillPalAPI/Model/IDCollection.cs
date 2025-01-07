using Microsoft.EntityFrameworkCore;
using PillPalAPI.Model;
using PillPalLib.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PillPalLib
{
    public class IDCollection<T> where T : class, IIdentified
    {
        private DbSet<T> values;
        public IEnumerable<T> Values => values.AsNoTracking().OrderBy(x => x.Id).Select(x => x);

        public IDCollection(DbSet<T> values)
        {
            this.values = values;
        }
        public int Size => Values.Count();
        public bool Remove(int id)
        {
            T? toRemove = Values.FirstOrDefault(x => x.Id == id);
            if (toRemove == null)
                return false;
            var context = values.Remove(toRemove!);
            context.Context.SaveChanges();
            return true;
        }
        public bool Add(T item)
        {
            if (Values.Any(x => x.Id == item.Id))
                return false;
            var context = values.Add(item);
            context.Context.SaveChanges();
            return true;
        }
        public bool Replace(T item)
        {
            if (!Values.Any(x => x.Id == item.Id))
                return false;
            var context = values.Update(item);
            context.Context.SaveChanges();
            return true;
        }
        public T? this[int id]
        {
            get => Values.FirstOrDefault(x => x.Id == id);
        }
    }
}
