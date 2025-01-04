using FluentValidation;
using PillPalLib;
using PillPalLib.DTOs.PackageSizeDTOs;

namespace PillPalAPI.Validators
{
    public class PackageSizeValidator : AbstractValidator<CreatePackageSizeDto>
    {
        public PackageSizeValidator() {
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("Package size has to be greater than 0.");
        }
    }
}
