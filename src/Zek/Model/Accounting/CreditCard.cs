using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;

namespace Zek.Model.Accounting
{
    public class CreditCard : BaseModel<int>
    {
        public string CardNumber { get; set; }
        public string HolderName { get; set; }

        public CardType? CardTypeId { get; set; }

        public string Cvc { get; set; }

        public byte ExpirationYear { get; set; }
        public short ExpirationMonth { get; set; }

    }
    public class CreditCardMap : BaseModelMap<CreditCard, int>
    {
        public CreditCardMap(ModelBuilder builder) : base(builder)
        {
            ToTable("CreditCards", "Accounting");

            Property(t => t.CardNumber).HasMaxLength(19).IsRequired();
            HasIndex(t => t.CardNumber);

            Property(t => t.HolderName).HasMaxLength(30).IsRequired();
            Property(t => t.Cvc).HasMaxLength(4);
        }
    }
}
