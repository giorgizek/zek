namespace Zek.Model.DTO.Identity
{
    [Obsolete]
    public class RegisterBaseDTO : CaptchaDTO
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? ConfirmPassword { get; set; }
    }
}
