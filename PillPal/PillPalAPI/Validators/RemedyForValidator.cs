using FluentValidation;
using PillPalLib.DTOs.RemedyForDTOs;

namespace PillPalAPI.Validators
{
    public class RemedyForValidator : AbstractValidator<CreateRemedyForDto>
    {
        public RemedyForValidator() {
            RuleFor(x => x.Ailment).MinimumLength(3);
        }
    }
}
