using System;
using Microsoft.EntityFrameworkCore;
using Zek.Model.Base;
using Zek.Model.DTO.Ecomm;

namespace Zek.Model.Accounting
{
    public class Ecomm : PocoModel<int>
    {
        public int MerchantId { get; set; }
        public string TransactionId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }//todo update database customerid
        public decimal Amount { get; set; }
        public decimal ReversalAmount { get; set; }
        public decimal Balance { get; set; }
        public int CurrencyId { get; set; }


        public string Description { get; set; }
        //public string Language { get; set; }

        public EcommResult ResultId { get; set; }
        public EcommResultPaymentServer ResultPaymentServerId { get; set; }
        public string ResultCode { get; set; }
        public EcommSecure3D Secure3DId { get; set; }
        public string Rrn { get; set; }
        public string ApprovalCode { get; set; }
        public string CardNumber { get; set; }
        public string Aav { get; set; }
        public string RegularPaymentId { get; set; }
        public DateTime? RegularPaymentExpiry { get; set; }
        public string MerchantTransactionId { get; set; }
        public string Error { get; set; }
        public string Response { get; set; }


        public string ClientIp { get; set; }
    }

    public class EcommMap : EcommMap<Ecomm>
    {
        public EcommMap(ModelBuilder builder) : base(builder)
        {
        }
    }

    public class EcommMap<TEntity> : PocoModelMap<TEntity, int> where TEntity : Ecomm
    {
        public EcommMap(ModelBuilder builder, bool? valueGeneratedOnAdd = null) : base(builder, valueGeneratedOnAdd)
        {
            ToTable("Ecomms", "Accounting");

            Property(e => e.TransactionId).HasMaxLength(28).IsRequired();
            HasIndex(e => new { e.MerchantId, e.TransactionId }).IsUnique();

            HasIndex(e => e.ApplicationId);
            HasIndex(e => e.CustomerId);
            Property(o => o.Date).HasColumnType("date");
            HasIndex(o => o.Date);

            Property(e => e.Amount).HasColumnType("decimal(18, 4)");
            Property(e => e.ReversalAmount).HasColumnType("decimal(18, 4)");
            Property(e => e.Balance).HasComputedColumnSql("[Amount]+[ReversalAmount]");
            Property(e => e.Description).HasMaxLength(125);

            Property(e => e.ResultCode).HasColumnType("varchar(3)");
            Property(e => e.Rrn).HasMaxLength(12);
            Property(e => e.ApprovalCode).HasMaxLength(6);
            Property(e => e.CardNumber).HasMaxLength(19);
            Property(e => e.Aav).HasMaxLength(400);
            Property(e => e.RegularPaymentId).HasMaxLength(28);
            Property(e => e.RegularPaymentExpiry).HasColumnType("date");
            Property(e => e.MerchantTransactionId).HasMaxLength(28);
            Property(e => e.ClientIp).HasColumnType("varchar(3)");
        }
    }

  
}
