
namespace Zek.Model.ViewModel.Membership
{
    public class VerifyCodeViewModel
    {
        public string Provider { get; set; }

        public string Code { get; set; }

        public string ReturnUrl { get; set; }

        public bool RememberBrowser { get; set; }

        public bool RememberMe { get; set; }
    }
}
