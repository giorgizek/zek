namespace Zek.OTP
{
    [Flags]
    public enum OtpStatus
    {
        Active = 1,
        Verified = 2,
        Expired = 3,
        TooManyAttempts = 4
    }
}