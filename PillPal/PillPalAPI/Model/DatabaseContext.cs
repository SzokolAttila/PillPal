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
            builder.Entity<MedicineSideEffect>()
                .HasOne(x => x.Medicine)
                .WithMany(x => x.SideEffects)
                .HasForeignKey(x => x.MedicineId);
            builder.Entity<MedicineSideEffect>()
                .HasOne(x => x.SideEffect)
                .WithMany(x => x.MedicineSideEffects)
                .HasForeignKey(x => x.SideEffectId);
        }
        public DbSet<SideEffect> SideEffects { get; set; }
        public DbSet<MedicineSideEffect> MedicineSideEffects { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
