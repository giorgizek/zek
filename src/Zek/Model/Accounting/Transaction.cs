using System;
using Microsoft.EntityFrameworkCore;
using Zek.Financial;
using Zek.Model.Base;

namespace Zek.Model.Accounting
{
   public class TransactionPoco : PocoModel<int>
    {
        public int AccountId { get; set; }

        /// <summary>
        /// If amount is recieved then IsCredit=true. If amount is send to other account then IsCredit=false
        /// </summary>
        public bool IsCredit { get; set; }
        public DateTime Date { get; set; }
        public decimal Amount { get; set; }
        public int CurrencyId { get; set; }

        public PaymentMethod PaymentMethodId { get; set; }

        public int TypeId { get; set; }

        public TransactionStatus StatusId { get; set; }

        public int? PersonId { get; set; }

        public string Description { get; set; }
    }

    public class TransactionPocoMap<TEntity> : PocoModelMap<TEntity, int>
        where TEntity : TransactionPoco
    {
        public TransactionPocoMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            ToTable("Transactions", "Accounting");
            Property(t => t.Date).HasColumnType("datetime2(0)");
            HasIndex(t => t.Date);

            HasIndex(t => t.StatusId);
            HasIndex(t => t.PersonId);

            Property(t => t.Description).HasMaxLength(400);
        }
    }
}
