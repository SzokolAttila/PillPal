using PillPalLib.Interfaces;

namespace PillPalLib
{
    public class Medicine : IIdentified
    {
        public Medicine(int id, string name, string description, int[] packageSizes, string manufacturer, string[] remedyFor, string[] activeIngredients, string[] sideEffects)
        {
            Id = id;
            Name = name;
            Description = description;
            PackageSizes = packageSizes;
            Manufacturer = manufacturer;
            RemedyFor = remedyFor;
            ActiveIngredients = activeIngredients;
            SideEffects = sideEffects;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int[] PackageSizes { get; set; }
        public string Manufacturer { get; set; }
        public string[] RemedyFor {  get; set; }
        public string[] ActiveIngredients { get; set; }
        public string[] SideEffects { get; set; }
    }
}
