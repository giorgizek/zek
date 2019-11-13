namespace Zek.Model.DTO.Identity
{
    public class ChangePasswordDTO
    {
        //public bool HasPassword { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }

       

        public string StatusMessage { get; set; }
    }
}
