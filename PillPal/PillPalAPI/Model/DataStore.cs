using Microsoft.EntityFrameworkCore;
using PillPalLib;

namespace PillPalAPI.Model
{
    public class DataStore : IDataStore { 
        public IDCollection<User> Users { get; }
        public IDCollection<Medicine> Medicines { get; }
        public IDCollection<Reminder> Reminders { get; }
        public DataStore(DatabaseContext context)
        {
            Users = new IDCollection<User>(context.Users);
            Medicines = new IDCollection<Medicine> (context.Medicines);
            Reminders = new IDCollection<Reminder> (context.Reminders);
        }
    }
}
