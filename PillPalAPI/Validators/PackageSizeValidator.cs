using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.PackageSizeDTOs;

namespace PillPalAPI.Validators
{
    public class PackageSizeValidator : AbstractValidator<CreatePackageSizeDto>
    {
        public PackageSizeValidator(IJoinStore<PackageSize> data) {
            RuleFor(x => x.Size).GreaterThan(0).WithMessage("A kiszerelésnek nullánál nagyobbnak kell lennie.");
            RuleFor(x => x).Must(dto => !data.GetAll().Any(y => y.Size == dto.Size && y.MedicineId == dto.MedicineId)).WithMessage("Ez a kiszerelés már hozzá lett adva ehhez a gyógyszerhez.");
        }
    }
}
