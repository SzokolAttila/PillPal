﻿using FluentValidation;
using PillPalLib;

namespace PillPalAPI.Validators
{
    public class ReminderValidator : AbstractValidator<Reminder>
    {
        public ReminderValidator() {
            RuleFor(x => x.DoseMg).GreaterThan(0).WithMessage("Cannot add medicine with negative dose");
            RuleFor(x => x.DoseCount).GreaterThan(0).WithMessage("Cannot add medicine with negative dose");
        }
    }
}
