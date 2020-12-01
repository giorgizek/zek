namespace Zek.Model.DTO.Identity
{
    public class RegisterDTO
    {
        //[Required(ErrorMessageResourceName =nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType =typeof(DataAnnotationsResources))]
        //[EmailAddress(ErrorMessageResourceName = nameof(DataAnnotationsResources.EmailAddressAttribute_Invalid), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        //[Display(Name = nameof(EmailResources.Email), ResourceType = typeof(EmailResources))]
        public string Email { get; set; }

        //[Required(ErrorMessageResourceName = nameof(DataAnnotationsResources.RequiredAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        //[StringLength(100, MinimumLength = 6, ErrorMessageResourceName = nameof(DataAnnotationsResources.StringLengthAttribute_ValidationError), ErrorMessageResourceType = typeof(DataAnnotationsResources))]
        //[DataType(DataType.Password)]
        //[Display(Name = nameof(MembershipResources.Password), ResourceType = typeof(MembershipResources))]
        public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = nameof(MembershipResources.ConfirmPassword), ResourceType = typeof(MembershipResources))]
        //[Compare(nameof(Password), ErrorMessageResourceName = nameof(MembershipResources.ConfirmPasswordErrorText), ErrorMessageResourceType = typeof(MembershipResources))]
        public string ConfirmPassword { get; set; }
    }
}
