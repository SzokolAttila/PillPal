﻿using PillPalLib;
using PillPalLib.Interfaces;

namespace PillPalAPI.Model
{
    public interface IItemStore<T> where T : IIdentified
    {
        IEnumerable<T> GetAll();
        bool Add(T item);
        T? Get(int id);
        bool Update(T item);
        bool Delete(int id);
    }
}
