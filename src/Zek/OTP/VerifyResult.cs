namespace Zek.OTP
{
    public class VerifyResult
    {
        public bool Succeeded { get; protected set; }
        public bool IsLockedOut { get; protected set; }
        public bool IsExpired { get; protected set; }

        public static VerifyResult Success { get; } = new() { Succeeded = true };

        public static VerifyResult Failed { get; } = new();

        public static VerifyResult Expired { get; } = new() { IsExpired = true };

        public static VerifyResult LockedOut { get; } = new() { IsLockedOut = true };

        public override string ToString()
        {
            return IsLockedOut ? "Lockedout" :
                IsExpired ? "Expired" :
                Succeeded ? "Succeeded" : "Failed";
        }
    }
}