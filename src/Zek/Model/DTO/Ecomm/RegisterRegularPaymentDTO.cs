namespace Zek.Model.DTO.Ecomm
{
    public class RegisterRegularPaymentDTO
    {
        /// <summary>
        /// RECC_PMNT_ID: rec_pmnt_id, if specified, othervise TRANSACTION_ID
        /// </summary>
        public string RegularPaymentId { get; set; }

        
        /// <summary>
        /// transaction identifier (28 characters in base64 encoding)
        /// </summary>
        public string TransactionId { get; set; }


        /// <summary>
        /// min (card expiry date, expiry parameters).
        /// </summary>
        public string RegularPaymentExpiry { get; set; }
    }
}
