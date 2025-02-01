namespace PillPalLib.DTOs.MedicineDTOs
{
    public class CreateMedicineDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int PackageUnitId { get; set; }
        public string Manufacturer { get; set; } = "";
    }
}
