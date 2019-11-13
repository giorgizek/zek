namespace Zek.Identity
{
    public enum ErrorCode
    {
        InvalidLoginAttempt = 1,
        IsLockedOut,
        InvalidToken,
        DuplicateEmail,
        InvalidIdLink,
    }
}
