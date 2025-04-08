using FluentValidation;
using PillPalLib.DTOs.MedicineDTOs;

namespace PillPalAPI.Validators
{
    public class MedicineValidator : AbstractValidator<CreateMedicineDto>
    {
        public MedicineValidator() {
            RuleFor(x => x.Name).Length(5, 30).WithMessage("A gyógyszer nevének 5 és 30 karakter között kell lennie.");
            RuleFor(x => x.Manufacturer).Length(3, 30).WithMessage("A gyártó nevének 3 és 30 karakter között kell lennie.");
            RuleFor(x => x.Description).Length(5, 255).WithMessage("A leírásnak 5 és 255 karakter között kell lennie.");
        }
    }
}
