using FluentValidation;
using PillPalLib;
using PillPalLib.DTOs.ReminderDTOs;

namespace PillPalAPI.Validators
{
    public class ReminderValidator : AbstractValidator<CreateReminderDto>
    {
        public ReminderValidator() {
            RuleFor(x => x.DoseCount).GreaterThan(0).WithMessage("Nem adhatsz meg emlékeztetőt nulla vagy negatív adaggal.");
        }
    }
}
