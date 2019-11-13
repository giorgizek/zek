using System;
using Zek.Model.Dictionary;

namespace Zek.Model.ViewModel.Membership
{
    public class MembershipPersonViewModel
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public Gender GenderId { get; set; }

        public DateTime? BirthDate { get; set; }
    }
}
