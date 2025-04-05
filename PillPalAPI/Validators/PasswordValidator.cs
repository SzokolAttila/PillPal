using FluentValidation;

namespace PillPalAPI.Validators
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public PasswordValidator() {
            RuleFor(x => x).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("A jelszavadnak legalább 8 karakter hosszúnak kell lennie," +
                " tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).");
        }
    }
}
