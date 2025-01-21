namespace Zek.Model.DTO.Ecomm
{
    public class EcommsDTO
    {
        public int Id { get; set; }

        public int MerchantId { get; set; }

        public int ApplicationId { get; set; }

        public string TransactionId { get; set; }
        
        public decimal Amount { get; set; }

        public decimal ReversalAmount { get; set; }

        public decimal Balance { get; set; }

        public ISO4217.ISO4217 CurrencyId { get; set; }

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
    }
}
