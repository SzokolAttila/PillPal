﻿using Microsoft.EntityFrameworkCore;
using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore : IDataStore
    {
        public IDCollection<User> Users { get; }
        public IDCollection<Medicine> Medicines { get; }
        public IDCollection<Reminder> Reminders { get; }
        public IDCollection<SideEffect> SideEffects {  get; }
        public IDCollection<MedicineSideEffect> MedicineSideEffects { get; }
        public IDCollection<ActiveIngredient> ActiveIngredients { get; }

        public IDCollection<MedicineActiveIngredient> MedicineActiveIngredients { get; }

        public IDCollection<RemedyFor> RemedyForAilments { get; }
        public IDCollection<MedicineRemedyFor> MedicineRemedyForAilments { get; }

        public IDCollection<PackageSize> PackageSizes { get; }

        public IDCollection<PackageUnit> PackageUnits { get; }

        public DataStore(DatabaseContext context)
        {
            Users = new IDCollection<User>(context.Users);
            Medicines = new IDCollection<Medicine> (context.Medicines);
            Reminders = new IDCollection<Reminder> (context.Reminders);
            SideEffects = new IDCollection<SideEffect> (context.SideEffects);
            MedicineSideEffects = new IDCollection<MedicineSideEffect> (context.MedicineSideEffects);
            ActiveIngredients = new IDCollection<ActiveIngredient> (context.ActiveIngredients);
            MedicineActiveIngredients = new IDCollection<MedicineActiveIngredient>(context.MedicineActiveIngredients);
            RemedyForAilments = new IDCollection<RemedyFor> (context.RemedyForAilments);
            MedicineRemedyForAilments = new IDCollection<MedicineRemedyFor>(context.MedicineRemedyForAilments);
            PackageSizes = new IDCollection<PackageSize> (context.PackageSizes);
            PackageUnits = new IDCollection<PackageUnit>(context.PackageUnits);
        }
    }
}
