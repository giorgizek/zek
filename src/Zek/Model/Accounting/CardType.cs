using System.ComponentModel.DataAnnotations;

namespace Zek.Model.Accounting
{
    public enum CardType
    {
        [Display(Name = "Visa")]
        Visa = 1,

        [Display(Name = "Master Card")]
        MasterCard = 2,

        [Display(Name = "American Express")]
        AmEx = 3,

        [Display(Name = "Discover")]
        Discover = 4
    }
}
