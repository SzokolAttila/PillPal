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
                .WithMany(x => x.SideEffects)
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
                .WithMany(x => x.ActiveIngredients)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<MedicineActiveIngredient>()
                .HasOne(x => x.ActiveIngredient)
                .WithMany(x => x.ActiveIngredients)
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
        }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<RemedyFor> RemedyForAilments { get; set; }
        public DbSet<MedicineSideEffect> MedicineSideEffects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<ActiveIngredient> ActiveIngredients { get; set; }
        public DbSet<MedicineActiveIngredient> MedicineActiveIngredients { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineRemedyFor> MedicineRemedyForAilments { get; set; }
    }
}
