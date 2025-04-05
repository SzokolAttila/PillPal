using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.SideEffectDTOs;

namespace PillPalAPI.Validators
{
    public class SideEffectValidator : AbstractValidator<CreateSideEffectDto>
    {
        public SideEffectValidator(IItemStore<SideEffect> data) 
        {
            RuleFor(x => x.Effect).MinimumLength(3).WithMessage("A mellékhatás neve túl rövid.");
            RuleFor(x => x.Effect).Must(x => !data.GetAll().Any(y => y.Effect == x)).WithMessage("A mellékhatás már létezik.");
        }
    }
}
