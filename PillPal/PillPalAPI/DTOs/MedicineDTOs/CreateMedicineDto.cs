namespace PillPalAPI.DTOs.MedicineDTOs
{
    public class CreateMedicineDto
    {
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int[] PackageSizes { get; set; } = new int[0];
        public string PackageUnit { get; set; } = "";
        public string Manufacturer { get; set; } = "";
        public string[] RemedyFor { get; set; } = new string[0];
        public string[] ActiveIngredients { get; set; } = new string[0];
        public string[] SideEffects { get; set; } = new string[0];
    }
}
