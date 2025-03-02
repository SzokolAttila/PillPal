using Microsoft.EntityFrameworkCore;
using PillPalLib;

namespace PillPalAPI.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<Reminder>().Navigation(x => x.User).AutoInclude();
            builder.Entity<Reminder>().Navigation(x => x.Medicine).AutoInclude();
            builder.Entity<Reminder>(x => x.HasKey(y => y.Id));
            builder.Entity<Reminder>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<Reminder>()
                .HasOne(x => x.User)
                .WithMany(x => x.Reminders)
                .HasForeignKey(x => x.UserId);

            builder.Entity<MedicineSideEffect>().Navigation(x => x.Medicine).AutoInclude();
            builder.Entity<MedicineSideEffect>().Navigation(x => x.SideEffect).AutoInclude();
            builder.Entity<MedicineSideEffect>().HasKey(x => x.Id);
            builder.Entity<MedicineSideEffect>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.MedicineSideEffects)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<MedicineSideEffect>()
                .HasOne(x => x.SideEffect)
                .WithMany(x => x.MedicineSideEffects)
                .HasForeignKey(x => x.SideEffectId);


            builder.Entity<MedicineActiveIngredient>().Navigation(x => x.Medicine).AutoInclude();
            builder.Entity<MedicineActiveIngredient>().Navigation(x => x.ActiveIngredient).AutoInclude();
            builder.Entity<MedicineActiveIngredient>().HasKey(x => x.Id);
            builder.Entity<MedicineActiveIngredient>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.MedicineActiveIngredients)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<MedicineActiveIngredient>()
                .HasOne(x => x.ActiveIngredient)
                .WithMany(x => x.MedicineActiveIngredients)
                .HasForeignKey(x => x.ActiveIngredientId);

            builder.Entity<MedicineRemedyFor>().Navigation(x => x.Medicine).AutoInclude();
            builder.Entity<MedicineRemedyFor>().Navigation(x => x.RemedyFor).AutoInclude();
            builder.Entity<MedicineRemedyFor>().HasKey(x => x.Id);
            builder.Entity<MedicineRemedyFor>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.MedicineRemedyForAilments)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<MedicineRemedyFor>()
                .HasOne(x => x.RemedyFor)
                .WithMany(x => x.MedicineRemedyForAilments)
                .HasForeignKey(x => x.RemedyForId);

            builder.Entity<Medicine>().Navigation(x => x.PackageSizeObjects).AutoInclude();

            builder.Entity<Medicine>()
                .HasMany(x => x.ActiveIngredientObjects)
                .WithMany(x => x.Medicines)
                .UsingEntity<MedicineActiveIngredient>();
            builder.Entity<Medicine>().Navigation(x => x.ActiveIngredientObjects).AutoInclude();

            builder.Entity<Medicine>()
                .HasMany(x => x.SideEffectObjects)
                .WithMany(x => x.Medicines)
                .UsingEntity<MedicineSideEffect>();
            builder.Entity<Medicine>().Navigation(x => x.SideEffectObjects).AutoInclude();

            builder.Entity<Medicine>()
                .HasMany(x => x.RemedyForObjects)
                .WithMany(x => x.Medicines)
                .UsingEntity<MedicineRemedyFor>();
            builder.Entity<Medicine>().Navigation(x => x.RemedyForObjects).AutoInclude();

            builder.Entity<SideEffect>().Navigation(x => x.Medicines).EnableLazyLoading();
            builder.Entity<RemedyFor>().Navigation(x => x.Medicines).EnableLazyLoading();
            builder.Entity<ActiveIngredient>().Navigation(x => x.Medicines).EnableLazyLoading();
            builder.Entity<SideEffect>().Navigation(x => x.Medicines).EnableLazyLoading();

            builder.Entity<PackageSize>().HasKey(x => x.Id);

            builder.Entity<PackageUnit>().HasKey(x => x.Id);

            builder.Entity<Medicine>()
                .HasOne(x => x.PackageUnit)
                .WithMany(x => x.Medicines)
                .HasForeignKey(x => x.PackageUnitId);
            builder.Entity<Medicine>().Navigation(x => x.PackageUnit).AutoInclude();

            Seed(builder);
        }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<PackageSize> PackageSizes { get; set; }
        public DbSet<RemedyFor> RemedyForAilments { get; set; }
        public DbSet<MedicineSideEffect> MedicineSideEffects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<MedicineActiveIngredient> MedicineActiveIngredients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineRemedyFor> MedicineRemedyForAilments { get; set; }
        public DbSet<PackageUnit> PackageUnits { get; set; }
        private void Seed(ModelBuilder modelBuilder)
        {
            var packageUnits = new List<PackageUnit>
            {
                new PackageUnit { Id = 1, Name = "tabletta" },
                new PackageUnit { Id = 2, Name = "ml" },
                new PackageUnit { Id = 3, Name = "injekció" }
            };
            modelBuilder.Entity<PackageUnit>().HasData(packageUnits);

            var medicines = new List<Medicine>
            {
                new Medicine
                {
                    Id = 1,
                    Name = "Humira",
                    Description = "Autoimmun betegségek kezelésére szolgáló biológiai gyógyszer.",
                    Manufacturer = "AbbVie",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 2,
                    Name = "Keytruda",
                    Description = "Immunterápiás gyógyszer különböző rákos megbetegedések kezelésére.",
                    Manufacturer = "Merck",
                    PackageUnitId = 2
                },
                new Medicine
                {
                    Id = 3,
                    Name = "Ozempic",
                    Description = "2-es típusú cukorbetegség és testsúlycsökkentés kezelésére szolgáló gyógyszer.",
                    Manufacturer = "Novo Nordisk",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 4,
                    Name = "Eliquis",
                    Description = "Antikoaguláns, amely a vérrögképződést gátolja.",
                    Manufacturer = "Bristol-Myers Squibb és Pfizer",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 5,
                    Name = "Dupixent",
                    Description = "Biológiai gyógyszer gyulladásos betegségek kezelésére.",
                    Manufacturer = "Sanofi és Regeneron",
                    PackageUnitId = 3
                }
            };
            modelBuilder.Entity<Medicine>().HasData(medicines);

            var sideEffects = new List<SideEffect>
            {
                new SideEffect { Id = 1, Effect = "Fejfájás" },
                new SideEffect { Id = 2, Effect = "Hányinger" },
                new SideEffect { Id = 3, Effect = "Fáradtság" },
                new SideEffect { Id = 4, Effect = "Bőrkiütés" },
                new SideEffect { Id = 5, Effect = "Hasmenés" }
            };
            modelBuilder.Entity<SideEffect>().HasData(sideEffects);

            var medicineSideEffects = new List<MedicineSideEffect>
            {
                new MedicineSideEffect { Id = 1, MedicineId = 1, SideEffectId = 1 },
                new MedicineSideEffect { Id = 2, MedicineId = 1, SideEffectId = 2 },
                new MedicineSideEffect { Id = 3, MedicineId = 2, SideEffectId = 3 },
                new MedicineSideEffect { Id = 4, MedicineId = 2, SideEffectId = 4 },
                new MedicineSideEffect { Id = 5, MedicineId = 3, SideEffectId = 5 },
                new MedicineSideEffect { Id = 6, MedicineId = 3, SideEffectId = 1 },
                new MedicineSideEffect { Id = 7, MedicineId = 4, SideEffectId = 2 },
                new MedicineSideEffect { Id = 8, MedicineId = 4, SideEffectId = 3 },
                new MedicineSideEffect { Id = 9, MedicineId = 5, SideEffectId = 4 },
                new MedicineSideEffect { Id = 10, MedicineId = 5, SideEffectId = 5 }
            };
            modelBuilder.Entity<MedicineSideEffect>().HasData(medicineSideEffects);

            var remedies = new List<RemedyFor>
            {
                new RemedyFor { Id = 1, Ailment = "Reumatoid artritisz" },
                new RemedyFor { Id = 2, Ailment = "Melanóma" },
                new RemedyFor { Id = 3, Ailment = "2-es típusú cukorbetegség" },
                new RemedyFor { Id = 4, Ailment = "Vérrögképződés" },
                new RemedyFor { Id = 5, Ailment = "Ekcéma" }
            };
            modelBuilder.Entity<RemedyFor>().HasData(remedies);

            var medicineRemedies = new List<MedicineRemedyFor>
            {
                new MedicineRemedyFor { Id = 1, MedicineId = 1, RemedyForId = 1 },
                new MedicineRemedyFor { Id = 2, MedicineId = 2, RemedyForId = 2 },
                new MedicineRemedyFor { Id = 3, MedicineId = 3, RemedyForId = 3 },
                new MedicineRemedyFor { Id = 4, MedicineId = 4, RemedyForId = 4 },
                new MedicineRemedyFor { Id = 5, MedicineId = 5, RemedyForId = 5 }
            };
           modelBuilder.Entity<MedicineRemedyFor>().HasData(medicineRemedies);

            var activeIngredients = new List<ActiveIngredient>
            {
                new ActiveIngredient { Id = 1, Ingredient = "Adalimumab" },
                new ActiveIngredient { Id = 2, Ingredient = "Pembrolizumab" },
                new ActiveIngredient { Id = 3, Ingredient = "Semaglutid" },
                new ActiveIngredient { Id = 4, Ingredient = "Apixaban" },
                new ActiveIngredient { Id = 5, Ingredient = "Dupilumab" }
            };
            modelBuilder.Entity<ActiveIngredient>().HasData(activeIngredients);

            var medicineActiveIngredients = new List<MedicineActiveIngredient>
            {
                new MedicineActiveIngredient { Id = 1, MedicineId = 1, ActiveIngredientId = 1 },
                new MedicineActiveIngredient { Id = 2, MedicineId = 2, ActiveIngredientId = 2 },
                new MedicineActiveIngredient { Id = 3, MedicineId = 3, ActiveIngredientId = 3 },
                new MedicineActiveIngredient { Id = 4, MedicineId = 4, ActiveIngredientId = 4 },
                new MedicineActiveIngredient { Id = 5, MedicineId = 5, ActiveIngredientId = 5 }
            };
            modelBuilder.Entity<MedicineActiveIngredient>().HasData(medicineActiveIngredients);

            var packageSizes = new List<PackageSize>
            {
                new PackageSize { Id = 1, MedicineId = 1, Size = 2 },
                new PackageSize { Id = 2, MedicineId = 2, Size = 4 },
                new PackageSize { Id = 3, MedicineId = 3, Size = 1 },
                new PackageSize { Id = 4, MedicineId = 4, Size = 10 },
                new PackageSize { Id = 5, MedicineId = 5, Size = 5 }
            };
            modelBuilder.Entity<PackageSize>().HasData(packageSizes);
        }
    }
}
