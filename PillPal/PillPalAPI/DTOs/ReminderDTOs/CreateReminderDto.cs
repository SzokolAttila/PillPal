namespace PillPalAPI.DTOs.ReminderDTOs
{
    public class CreateReminderDto
    {
        public int UserId { get; set; }
        public int MedicineId { get; set; }
        public TimeOnly When { get; set; }
        public double DoseCount { get; set; }
        public int DoseMg { get; set; }
        public string TakingMethod { get; set; } = "";
    }
}
