using Microsoft.EntityFrameworkCore;
using PillPalLib;

namespace PillPalAPI.Model
{
    public class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<Medicine> Medicines { get; set; }
    }
}
