using FluentValidation;
using PillPalLib;
using PillPalLib.DTOs.SideEffectDTOs;

namespace PillPalAPI.Validators
{
    public class SideEffectValidator : AbstractValidator<CreateSideEffectDto>
    {
        public SideEffectValidator() 
        {
            RuleFor(x => x.Effect).MinimumLength(3);
        }
    }
}
