using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.PackageUnitDTOs;

namespace PillPalAPI.Validators
{
    public class PackageUnitValidator : AbstractValidator<CreatePackageUnitDto>
    {
        public PackageUnitValidator() {
            RuleFor(x => x.Name).Length(1, 20).WithMessage("A kicsomagolás mértékegységének 1 és 20 karakter között kell lennie.");
        }
    }
}
