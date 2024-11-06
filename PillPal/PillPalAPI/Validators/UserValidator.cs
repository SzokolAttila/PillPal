using FluentValidation;
using PillPalAPI.DTOs.UserDTOs;

namespace PillPalAPI.Validators
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator()
        {
            RuleFor(x => x.UserName).Length(6, 20).WithMessage("Your username needs to be between 6 and 20 characters.");
            RuleFor(x => x.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("Your password needs to include at least 8 characters, " +
                "both upper and lowercase letters, a number, and a special character (@$!%*?&).");
        }
    }
}
