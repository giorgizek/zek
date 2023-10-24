using Zek.Utils;

namespace Zek.Model.Config
{
    public class OtpOptions
    {
        public int ExpireMinutes { get; set; } = 3;
        public int CharLength { get; set; } = 4;
        public string Chars { get; set; } = PasswordHelper.Numbers;
        public int MaxFailedAccessAttempts { get; set; } = 3;
    }
}