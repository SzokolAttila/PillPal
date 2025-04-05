using FluentValidation;
using PillPalLib.DTOs.UserDTOs;
using PillPalAPI.Model;
using PillPalLib;

namespace PillPalAPI.Validators
{
    public class UserValidator : AbstractValidator<CreateUserDto>
    {
        public UserValidator(IItemStore<User> data)
        {
            RuleFor(x => x.UserName).Length(6, 20).WithMessage("A felhasználónévnek 6 és 20 karakter között kell lennie.");
            RuleFor(x => x.UserName).Must(x => !data.GetAll().Any(y => y.UserName == x)).WithMessage("A felhasználónév már létezik.");
            RuleFor(x => x.UserName).Must(x => x.All(y => char.IsAsciiLetterOrDigit(y))).WithMessage("A felhasználónév csak betűket és számokat tartalmazhat.");
            RuleFor(x => x.Password).Matches("^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$")
                .WithMessage("A jelszavadnak legalább 8 karakter hosszúnak kell lennie," +
                " tartalmaznia kell kis- és nagybetűket, számot és speciális karaktert (@$!%*?&).");
        }
    }
}
