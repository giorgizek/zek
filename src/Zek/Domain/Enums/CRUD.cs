namespace Zek.Domain.Enums
{
    [Flags]
    public enum CRUD
    {
        Create = 1,
        Read = 2,
        Update = 4,
        Delete = 8
    }
}
