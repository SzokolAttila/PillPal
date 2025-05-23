﻿using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.RemedyForDTOs;

namespace PillPalAPI.Validators
{
    public class RemedyForValidator : AbstractValidator<CreateRemedyForDto>
    {
        public RemedyForValidator(IItemStore<RemedyFor> data) {
            RuleFor(x => x.Ailment).MinimumLength(3).WithMessage("A betegség neve túl rövid.");
            RuleFor(x => x.Ailment).Must(x => !data.GetAll().Any(y => y.Ailment == x)).WithMessage("A betegség már létezik.");
        }
    }
}
