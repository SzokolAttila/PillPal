using FluentValidation;
using PillPalAPI.Model;
using PillPalLib;
using PillPalLib.DTOs.ActiveIngredientDTOs;

namespace PillPalAPI.Validators
{
    public class ActiveIngredientValidator : AbstractValidator<CreateActiveIngredientDto>
    {
        public ActiveIngredientValidator(IItemStore<ActiveIngredient> data) 
        {
            RuleFor(x => x.Ingredient).MinimumLength(3);
            RuleFor(x => x.Ingredient).Must(x => !data.GetAll().Any(y => y.Ingredient == x)).WithMessage("Active ingredient already added.");
        }
    }
}
