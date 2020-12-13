using System;

namespace Zek.OTP
{
    [Flags]
    public enum OtpStatus
    {
        Generated = 1,
        Sent = 2,
        Success = 4,
        LockedOut = 8
    }
}