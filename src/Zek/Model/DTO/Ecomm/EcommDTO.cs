namespace Zek.Model.DTO.Ecomm
{
    public class EcommDTO
    {
        public int MerchantId { get; set; }
        public string TransactionId { get; set; }
        public int ApplicationId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }
        public decimal Amount { get; set; }
        public decimal ReversalAmount { get; set; }
        public decimal Balance { get; set; }
        public ISO4217.ISO4217 CurrencyId { get; set; }


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
}
