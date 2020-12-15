namespace Zek.Model.DTO.Identity
{
    public class LoginBaseDTO : CaptchaDTO
    {
        public string Password { get; set; }

        public bool RememberMe { get; set; }
        public string Ip { get; set; }
    }
}