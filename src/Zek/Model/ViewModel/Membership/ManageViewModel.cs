using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using Zek.Model.DTO.Identity;

namespace Zek.Model.ViewModel.Membership
{
    public class ManageViewModel
    {
        public string Email { get; set; }

        public bool HasPassword { get; set; }

        public IList<UserLoginInfo> Logins { get; set; }

        public string PhoneNumber { get; set; }

        public bool TwoFactor { get; set; }

        public bool BrowserRemembered { get; set; }

        public MembershipPersonViewModel Person { get; set; }

        public ChangePasswordDTO ChangePassword { get; set; }
    }
}
