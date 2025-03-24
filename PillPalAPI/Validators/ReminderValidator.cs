using FluentValidation;
using PillPalLib;
using PillPalLib.DTOs.ReminderDTOs;

namespace PillPalAPI.Validators
{
    public class ReminderValidator : AbstractValidator<CreateReminderDto>
    {
        public ReminderValidator() {
            RuleFor(x => x.DoseCount).GreaterThan(0).WithMessage("Cannot add medicine with negative dose");
        }
    }
}
