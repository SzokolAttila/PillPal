namespace PillPalAPI.DTOs.UserDTOs
{
    public class CreateUserDto
    {
        public string UserName { get; set; } = string.Empty;
        public string PasswordText { get; set; } = string.Empty;
    }
}
