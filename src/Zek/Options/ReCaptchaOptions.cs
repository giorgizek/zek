namespace Zek.Options
{
    public class ReCaptchaOptions
    {
        public string SiteKey { get; set; } = string.Empty;
        public string SecretKey { get; set; } = string.Empty;
        public string VerifyingUrl { get; set; } = string.Empty;
    }
}
