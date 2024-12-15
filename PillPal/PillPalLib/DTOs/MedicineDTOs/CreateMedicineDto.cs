namespace PillPalLib.DTOs.MedicineDTOs
{
    public class CreateMedicineDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int[] PackageSizes { get; set; } = [];
        public string PackageUnit { get; set; } = "";
        public string Manufacturer { get; set; } = "";
    }
}
