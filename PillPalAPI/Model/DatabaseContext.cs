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

            builder.Entity<User>().Navigation(x => x.Reminders).AutoInclude();
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
                new PackageUnit { Id = 3, Name = "injekció" },
                new PackageUnit { Id = 4, Name = "kapszula" },
                new PackageUnit { Id = 5, Name = "tasak" }
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
                },
                new Medicine
                {
                    Id = 6,
                    Name = "Amoxicillin 750 mg",
                    Description = "Széles spektrumú antibiotikum, bakteriális fertőzések kezelésére, mint például légúti, húgyúti és bőrfertőzések.",
                    Manufacturer = "Teva",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 7,
                    Name = "Amoxicillin 1000 mg",
                    Description = "Széles spektrumú antibiotikum, bakteriális fertőzések kezelésére, mint például légúti, húgyúti és bőrfertőzések.",
                    Manufacturer = "Teva",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 8,
                    Name = "Béres VITA-D3 4000 NE",
                    Description = "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.",
                    Manufacturer = "Béres",
                    PackageUnitId = 4
                },
                new Medicine
                {
                    Id = 9,
                    Name = "Béres VITA-D3 1600 NE",
                    Description = "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.",
                    Manufacturer = "Béres",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 10,
                    Name = "Béres VITA-D3 2000 NE",
                    Description = "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.",
                    Manufacturer = "Béres",
                    PackageUnitId = 4
                },
                new Medicine
                {
                    Id = 11,
                    Name = "Béres VITA-D3 FORTE 3200 NE",
                    Description = "Hozzájárul az egészséges csontozat, fogazat és izomfunkciók fenntartásához, emellett szerepet játszik az immunrendszer megfelelő működésében is.",
                    Manufacturer = "Béres",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 12,
                    Name = "Zitrocin 500 mg",
                    Description = "Fertőző betegségek kezelésére szolgáló antibiotikum, ami gátolja a baktériumok fehérjeszintézisét, ezáltal megakadályozza a baktériumok növekedését és szaporodását.",
                    Manufacturer = "Teva",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 13,
                    Name = "Aspirin Ultra",
                    Description = "Mikroaktív technológiájának köszönhetően a leggyorsabb felszívódású Aspirin. Hatékonyan csillapítja a fejfájást és a lázat.",
                    Manufacturer = "Bayer",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 14,
                    Name = "Aspirin Complex",
                    Description = "Enyhíti a megfázás okozta fájdalmakat, csillapítja a lázat és még az orrdugulást is megszünteti.",
                    Manufacturer = "Bayer",
                    PackageUnitId = 5
                },
                new Medicine
                {
                    Id = 15,
                    Name = "Aspirin Complex Forró ital",
                    Description = "Enyhíti a megfázás okozta fájdalmakat, csillapítja a lázat és még az orrdugulást is megszünteti.",
                    Manufacturer = "Bayer",
                    PackageUnitId = 5
                },
                new Medicine
                {
                    Id = 16,
                    Name = "Aspirin Effect",
                    Description = "A fejfájás nem válogat, bárhol, bármikor megjelenhet. Készüljön fel rá az Aspirin Effect-tel!",
                    Manufacturer = "Bayer",
                    PackageUnitId = 5
                },
                new Medicine
                {
                    Id = 17,
                    Name = "Aspirin Plus C",
                    Description = "Hatékonyan enyhíti a megfázás első jeleinek fájdalmait, csillapítja a lázat és még C-vitamint is tartalmaz.",
                    Manufacturer = "Bayer",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 18,
                    Name = "Aspirin Plus C Forte",
                    Description = "Hatékonyan enyhíti az erősebb megfázáshoz kapcsolódó fájdalmakat is, csillapítja a lázat és még C-vitamint is tartalmaz. Dupla hatóanyagtartalommal!",
                    Manufacturer = "Bayer",
                    PackageUnitId = 1
                },
                new Medicine
                {
                    Id = 19,
                    Name = "Aspirin Protect",
                    Description = "Megakadályozza a vérlemezkék összetapadását, s ezáltal az ereket szűkítő vagy elzáró vérrögök képződését.",
                    Manufacturer = "Bayer",
                    PackageUnitId = 1
                }
            };
            modelBuilder.Entity<Medicine>().HasData(medicines);

            var sideEffects = new List<SideEffect>
            {
                new SideEffect { Id = 1, Effect = "Fejfájás" },
                new SideEffect { Id = 2, Effect = "Hányinger" },
                new SideEffect { Id = 3, Effect = "Fáradtság" },
                new SideEffect { Id = 4, Effect = "Bőrkiütés" },
                new SideEffect { Id = 5, Effect = "Hasmenés" },
                new SideEffect { Id = 6, Effect = "Émelygés" },
                new SideEffect { Id = 7, Effect = "Magas kálciumszint" },
                new SideEffect { Id = 8, Effect = "Hasi fájdalom" },
                new SideEffect { Id = 9, Effect = "Vérzékenység" }
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
                new MedicineSideEffect { Id = 10, MedicineId = 5, SideEffectId = 5 },
                new MedicineSideEffect { Id = 11, MedicineId = 6, SideEffectId = 4 },
                new MedicineSideEffect { Id = 12, MedicineId = 6, SideEffectId = 5 },
                new MedicineSideEffect { Id = 13, MedicineId = 6, SideEffectId = 6 },
                new MedicineSideEffect { Id = 14, MedicineId = 7, SideEffectId = 4 },
                new MedicineSideEffect { Id = 15, MedicineId = 7, SideEffectId = 5 },
                new MedicineSideEffect { Id = 16, MedicineId = 7, SideEffectId = 6 },
                new MedicineSideEffect { Id = 17, MedicineId = 8, SideEffectId = 7 },
                new MedicineSideEffect { Id = 18, MedicineId = 9, SideEffectId = 7 },
                new MedicineSideEffect { Id = 19, MedicineId = 10, SideEffectId = 7 },
                new MedicineSideEffect { Id = 20, MedicineId = 11, SideEffectId = 7 },
                new MedicineSideEffect { Id = 21, MedicineId = 12, SideEffectId = 2 },
                new MedicineSideEffect { Id = 22, MedicineId = 12, SideEffectId = 5 },
                new MedicineSideEffect { Id = 23, MedicineId = 12, SideEffectId = 8 },
                new MedicineSideEffect { Id = 24, MedicineId = 13, SideEffectId = 8 },
                new MedicineSideEffect { Id = 25, MedicineId = 13, SideEffectId = 9 },
                new MedicineSideEffect { Id = 26, MedicineId = 14, SideEffectId = 8 },
                new MedicineSideEffect { Id = 27, MedicineId = 14, SideEffectId = 9 },
                new MedicineSideEffect { Id = 28, MedicineId = 15, SideEffectId = 8 },
                new MedicineSideEffect { Id = 29, MedicineId = 15, SideEffectId = 9 },
                new MedicineSideEffect { Id = 30, MedicineId = 16, SideEffectId = 8 },
                new MedicineSideEffect { Id = 31, MedicineId = 16, SideEffectId = 9 },
                new MedicineSideEffect { Id = 32, MedicineId = 17, SideEffectId = 8 },
                new MedicineSideEffect { Id = 33, MedicineId = 17, SideEffectId = 9 },
                new MedicineSideEffect { Id = 34, MedicineId = 18, SideEffectId = 8 },
                new MedicineSideEffect { Id = 35, MedicineId = 18, SideEffectId = 9 },
                new MedicineSideEffect { Id = 36, MedicineId = 19, SideEffectId = 8 },
                new MedicineSideEffect { Id = 37, MedicineId = 19, SideEffectId = 9 },
            };
            modelBuilder.Entity<MedicineSideEffect>().HasData(medicineSideEffects);

            var remedies = new List<RemedyFor>
            {
                new RemedyFor { Id = 1, Ailment = "Reumatoid artritisz" },
                new RemedyFor { Id = 2, Ailment = "Melanóma" },
                new RemedyFor { Id = 3, Ailment = "2-es típusú cukorbetegség" },
                new RemedyFor { Id = 4, Ailment = "Vérrögképződés" },
                new RemedyFor { Id = 5, Ailment = "Ekcéma" },
                new RemedyFor { Id = 6, Ailment = "Légúti fertőzések" },
                new RemedyFor { Id = 7, Ailment = "Húgyúti fertőzések" },
                new RemedyFor { Id = 8, Ailment = "Bőrfertőzések" },
                new RemedyFor { Id = 9, Ailment = "D-vitamin-hiány" },
                new RemedyFor { Id = 10, Ailment = "Csontritkulás" },
                new RemedyFor { Id = 11, Ailment = "Angolkór" },
                new RemedyFor { Id = 12, Ailment = "Nemi úton terjedő betegségek" },
                new RemedyFor { Id = 13, Ailment = "Fejfájás" },
                new RemedyFor { Id = 14, Ailment = "Megfázás" },
                new RemedyFor { Id = 15, Ailment = "Láz" },
                new RemedyFor { Id = 16, Ailment = "Torokfájás" }
            };
            modelBuilder.Entity<RemedyFor>().HasData(remedies);

            var medicineRemedies = new List<MedicineRemedyFor>
            {
                new MedicineRemedyFor { Id = 1, MedicineId = 1, RemedyForId = 1 },
                new MedicineRemedyFor { Id = 2, MedicineId = 2, RemedyForId = 2 },
                new MedicineRemedyFor { Id = 3, MedicineId = 3, RemedyForId = 3 },
                new MedicineRemedyFor { Id = 4, MedicineId = 4, RemedyForId = 4 },
                new MedicineRemedyFor { Id = 5, MedicineId = 5, RemedyForId = 5 },
                new MedicineRemedyFor { Id = 6, MedicineId = 6, RemedyForId = 6 },
                new MedicineRemedyFor { Id = 7, MedicineId = 6, RemedyForId = 7 },
                new MedicineRemedyFor { Id = 8, MedicineId = 6, RemedyForId = 8 },
                new MedicineRemedyFor { Id = 9, MedicineId = 7, RemedyForId = 6 },
                new MedicineRemedyFor { Id = 10, MedicineId = 7, RemedyForId = 7 },
                new MedicineRemedyFor { Id = 11, MedicineId = 7, RemedyForId = 8 },
                new MedicineRemedyFor { Id = 12, MedicineId = 8, RemedyForId = 9 },
                new MedicineRemedyFor { Id = 13, MedicineId = 8, RemedyForId = 10 },
                new MedicineRemedyFor { Id = 14, MedicineId = 8, RemedyForId = 11 },
                new MedicineRemedyFor { Id = 15, MedicineId = 9, RemedyForId = 9 },
                new MedicineRemedyFor { Id = 16, MedicineId = 9, RemedyForId = 10 },
                new MedicineRemedyFor { Id = 17, MedicineId = 9, RemedyForId = 11 },
                new MedicineRemedyFor { Id = 18, MedicineId = 10, RemedyForId = 9 },
                new MedicineRemedyFor { Id = 19, MedicineId = 10, RemedyForId = 10 },
                new MedicineRemedyFor { Id = 20, MedicineId = 10, RemedyForId = 11 },
                new MedicineRemedyFor { Id = 21, MedicineId = 11, RemedyForId = 9 },
                new MedicineRemedyFor { Id = 22, MedicineId = 11, RemedyForId = 10 },
                new MedicineRemedyFor { Id = 23, MedicineId = 11, RemedyForId = 11 },
                new MedicineRemedyFor { Id = 24, MedicineId = 12, RemedyForId = 6 },
                new MedicineRemedyFor { Id = 25, MedicineId = 12, RemedyForId = 7 },
                new MedicineRemedyFor { Id = 26, MedicineId = 12, RemedyForId = 8 },
                new MedicineRemedyFor { Id = 27, MedicineId = 12, RemedyForId = 12 },
                new MedicineRemedyFor { Id = 28, MedicineId = 13, RemedyForId = 13 },
                new MedicineRemedyFor { Id = 29, MedicineId = 14, RemedyForId = 14 },
                new MedicineRemedyFor { Id = 30, MedicineId = 14, RemedyForId = 15 },
                new MedicineRemedyFor { Id = 31, MedicineId = 15, RemedyForId = 14 },
                new MedicineRemedyFor { Id = 32, MedicineId = 15, RemedyForId = 15 },
                new MedicineRemedyFor { Id = 33, MedicineId = 16, RemedyForId = 13 },
                new MedicineRemedyFor { Id = 34, MedicineId = 17, RemedyForId = 13 },
                new MedicineRemedyFor { Id = 35, MedicineId = 17, RemedyForId = 15 },
                new MedicineRemedyFor { Id = 36, MedicineId = 17, RemedyForId = 16 },
                new MedicineRemedyFor { Id = 37, MedicineId = 18, RemedyForId = 13 },
                new MedicineRemedyFor { Id = 38, MedicineId = 18, RemedyForId = 15 },
                new MedicineRemedyFor { Id = 39, MedicineId = 18, RemedyForId = 16 },
                new MedicineRemedyFor { Id = 40, MedicineId = 19, RemedyForId = 4 }
            };
           modelBuilder.Entity<MedicineRemedyFor>().HasData(medicineRemedies);

            var activeIngredients = new List<ActiveIngredient>
            {
                new ActiveIngredient { Id = 1, Ingredient = "Adalimumab" },
                new ActiveIngredient { Id = 2, Ingredient = "Pembrolizumab" },
                new ActiveIngredient { Id = 3, Ingredient = "Semaglutid" },
                new ActiveIngredient { Id = 4, Ingredient = "Apixaban" },
                new ActiveIngredient { Id = 5, Ingredient = "Dupilumab" },
                new ActiveIngredient { Id = 6, Ingredient = "Amoxicillin" },
                new ActiveIngredient { Id = 7, Ingredient = "Klavulánsav" },
                new ActiveIngredient { Id = 8, Ingredient = "Kolekalciferol" },
                new ActiveIngredient { Id = 9, Ingredient = "Azitormicin" },
                new ActiveIngredient { Id = 10, Ingredient = "Acetilszalicilsav" },
                new ActiveIngredient { Id = 11, Ingredient = "Aszkorbinsav" }
            };
            modelBuilder.Entity<ActiveIngredient>().HasData(activeIngredients);

            var medicineActiveIngredients = new List<MedicineActiveIngredient>
            {
                new MedicineActiveIngredient { Id = 1, MedicineId = 1, ActiveIngredientId = 1 },
                new MedicineActiveIngredient { Id = 2, MedicineId = 2, ActiveIngredientId = 2 },
                new MedicineActiveIngredient { Id = 3, MedicineId = 3, ActiveIngredientId = 3 },
                new MedicineActiveIngredient { Id = 4, MedicineId = 4, ActiveIngredientId = 4 },
                new MedicineActiveIngredient { Id = 5, MedicineId = 5, ActiveIngredientId = 5 },
                new MedicineActiveIngredient { Id = 6, MedicineId = 6, ActiveIngredientId = 6 },
                new MedicineActiveIngredient { Id = 7, MedicineId = 6, ActiveIngredientId = 7 },
                new MedicineActiveIngredient { Id = 8, MedicineId = 7, ActiveIngredientId = 6 },
                new MedicineActiveIngredient { Id = 9, MedicineId = 7, ActiveIngredientId = 7 },
                new MedicineActiveIngredient { Id = 10, MedicineId = 8, ActiveIngredientId = 8 },
                new MedicineActiveIngredient { Id = 11, MedicineId = 9, ActiveIngredientId = 8 },
                new MedicineActiveIngredient { Id = 12, MedicineId = 10, ActiveIngredientId = 8 },
                new MedicineActiveIngredient { Id = 13, MedicineId = 11, ActiveIngredientId = 8 },
                new MedicineActiveIngredient { Id = 14, MedicineId = 12, ActiveIngredientId = 9 },
                new MedicineActiveIngredient { Id = 15, MedicineId = 13, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 16, MedicineId = 14, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 17, MedicineId = 15, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 18, MedicineId = 16, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 19, MedicineId = 17, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 20, MedicineId = 17, ActiveIngredientId = 11 },
                new MedicineActiveIngredient { Id = 21, MedicineId = 18, ActiveIngredientId = 10 },
                new MedicineActiveIngredient { Id = 22, MedicineId = 18, ActiveIngredientId = 11 },
                new MedicineActiveIngredient { Id = 23, MedicineId = 19, ActiveIngredientId = 10 },
            };
            modelBuilder.Entity<MedicineActiveIngredient>().HasData(medicineActiveIngredients);

            var packageSizes = new List<PackageSize>
            {
                new PackageSize { Id = 1, MedicineId = 1, Size = 2 },
                new PackageSize { Id = 2, MedicineId = 2, Size = 4 },
                new PackageSize { Id = 3, MedicineId = 3, Size = 1 },
                new PackageSize { Id = 4, MedicineId = 4, Size = 10 },
                new PackageSize { Id = 5, MedicineId = 5, Size = 5 },
                new PackageSize { Id = 6, MedicineId = 6, Size = 10 },
                new PackageSize { Id = 7, MedicineId = 6, Size = 20 },
                new PackageSize { Id = 8, MedicineId = 7, Size = 10 },
                new PackageSize { Id = 9, MedicineId = 7, Size = 20 },
                new PackageSize { Id = 10, MedicineId = 8, Size = 60 },
                new PackageSize { Id = 11, MedicineId = 9, Size = 90 },
                new PackageSize { Id = 12, MedicineId = 10, Size = 120 },
                new PackageSize { Id = 13, MedicineId = 11, Size = 60 },
                new PackageSize { Id = 14, MedicineId = 11, Size = 120 },
                new PackageSize { Id = 15, MedicineId = 12, Size = 3 },
                new PackageSize { Id = 16, MedicineId = 13, Size = 8 },
                new PackageSize { Id = 17, MedicineId = 14, Size = 10 },
                new PackageSize { Id = 18, MedicineId = 15, Size = 10 },
                new PackageSize { Id = 19, MedicineId = 16, Size = 10 },
                new PackageSize { Id = 20, MedicineId = 17, Size = 10 },
                new PackageSize { Id = 21, MedicineId = 18, Size = 10 },
                new PackageSize { Id = 22, MedicineId = 19, Size = 28 },
            };
            modelBuilder.Entity<PackageSize>().HasData(packageSizes);
        }
    }
}
