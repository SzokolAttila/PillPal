using FluentValidation;

namespace PillPalAPI.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator() {
            RuleFor(x => x).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("Your password needs to include at least 8 characters, " +
                    "both upper and lowercase letters, a number, and a special character (@$!%*?&).");
        }
    }
}
