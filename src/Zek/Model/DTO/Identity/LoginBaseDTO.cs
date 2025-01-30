namespace Zek.Model.DTO.Identity
{
    [Obsolete]
    public class LoginBaseDTO : CaptchaDTO
    {
        public string? Password { get; set; }

        public bool RememberMe { get; set; }
        public string? Ip { get; set; }
    }
}