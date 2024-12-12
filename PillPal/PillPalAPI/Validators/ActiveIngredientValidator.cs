using FluentValidation;
using PillPalLib.DTOs.ActiveIngredientDTOs;

namespace PillPalAPI.Validators
{
    public class ActiveIngredientValidator : AbstractValidator<CreateActiveIngredientDto>
    {
        public ActiveIngredientValidator() 
        {
            RuleFor(x => x.Ingredient).MinimumLength(3);
        }
    }
}
