using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.PackageSizeDTOs;

namespace PillPalAPI.Validators
{
    public class PackageSizeValidator : AbstractValidator<CreatePackageSizeDto>
    {
        public PackageSizeValidator(IJoinStore<PackageSize> data) {
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("Package size has to be greater than 0.");
            RuleFor(x => x).Must(dto => !data.GetAll().Any(y => y.Size == dto.Size && y.MedicineId == dto.MedicineId)).WithMessage("This PackageSize has already been added to this Medicine.");
        }
    }
}
