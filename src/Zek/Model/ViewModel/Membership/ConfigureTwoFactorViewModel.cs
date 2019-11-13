using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Zek.Model.ViewModel.Membership
{
    public class ConfigureTwoFactorViewModel
    {
        public string SelectedProvider { get; set; }

        public ICollection<SelectListItem> Providers { get; set; }
    }
}
