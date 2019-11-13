namespace Zek.Model.ViewModel
{
    public enum ValidationResult
    {
        Valid = 0,

        Required,
        StringLength,
        Range,
        Email,
        Phone,
        Url,

        NotFound,
        Restricted,
        IsApproved,
        IsDeleted
    }
}
