namespace Zek.Identity
{
    public enum ErrorCode
    {
        InvalidLoginAttempt = 1,
        IsLockedOut,
        Forbidden,
        InvalidToken,
        DuplicateEmail,
        InvalidIdLink,
        AccountDisabled
    }
}
