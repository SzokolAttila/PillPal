using FluentValidation;
using PillPalLib.DTOs.MedicineDTOs;

namespace PillPalAPI.Validators
{
    public class MedicineValidator : AbstractValidator<CreateMedicineDto>
    {
        public MedicineValidator() {
            RuleFor(x => x.Name).Length(5, 30).WithMessage("Medicine name must be between 5 and 30 characters.");
            RuleFor(x => x.Manufacturer).Length(5, 30).WithMessage("Manufacturer name must be between 5 and 30 characters.");
        }
    }
}
