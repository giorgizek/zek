using System;
using Microsoft.EntityFrameworkCore;
using Zek.Data.Entity;
using Zek.Model.Base;

namespace Zek.Model.Accounting
{
    public class AccountPoco : PocoModel
    {
        public int PersonId { get; set; }

        public string FriendlyName { get; set; }

        public DateTime OpenDate { get; set; }
        public DateTime? CloseDate { get; set; }

        public int TypeId { get; set; }

        public decimal Balance { get; set; }
        //public decimal BalanceInGel { get; }

        //bool CanBePrimary

        //paymentOperationTypes ["4.31.01.06-01", "4.31.01.11-01", "4.31.01.16-01", "4.31.01.17-01", "4.31.03.02-01", "4.31.03.06-01",…]

        public string AccountNumber { get; set; }

        public int CurrencyId { get; set; }

        public bool IsPrimary { get; set; }
        public bool IsHidden { get; set; }
        public bool IsClosed { get; set; }
    }

    public class AccountPocoMap<TEntity> : PocoModelMap<TEntity>
        where TEntity : AccountPoco
    {
        public AccountPocoMap(ModelBuilder builder) : base(builder)
        {
            ToTable("Accounts", "Accounting");

            Property(x => x.Balance).HasColumnTypeFinAmount();
            Property(x => x.AccountNumber).HasMaxLength(50);
            Property(x => x.FriendlyName).HasMaxLength(100);
            Property(x => x.OpenDate).HasColumnTypeDate();
            Property(x => x.CloseDate).HasColumnTypeDate();

            HasIndex(x => x.AccountNumber);
            HasIndex(x => x.PersonId);
            HasIndex(x => x.TypeId);
            HasIndex(x => x.IsClosed);
        }
    }
}
