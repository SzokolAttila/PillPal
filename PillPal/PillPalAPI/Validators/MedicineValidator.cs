using FluentValidation;
using PillPalLib.DTOs.MedicineDTOs;

namespace PillPalAPI.Validators
{
    public class MedicineValidator : AbstractValidator<CreateMedicineDto>
    {
        public MedicineValidator() {
            RuleFor(x => x.Name).Length(5, 30).WithMessage("Medicine name must be between 5 and 30 characters.");
            RuleFor(x => x.Manufacturer).Length(5, 30).WithMessage("Manufacturer name must be between 5 and 30 characters.");
            RuleFor(x => x.PackageSizes).Must(x=>x.Count()>0).WithMessage("There must be at least one package size.");
            RuleFor(x => x.PackageUnit).Must(x=>x=="mg" || x=="ml").WithMessage("Package unit must be either mg or ml.");
        }
    }
}
