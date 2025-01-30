namespace Zek.Model.DTO.Ecomm
{
    public class Credit
    {
        public string? ResultText { get; set; }
        public EcommResult Result { get; set; }

        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string? ResultCode { get; set; }


        /// <summary>
        /// refund transaction identifier – applicable for obtaining refund payment details or to request refund payment reversal.
        /// </summary>
        public string? RefundTransactionId { get; set; }


        /// <summary>
        /// In case of an error, the returned string of symbols begins with ‘error:‘
        /// </summary>
        public string? Error { get; set; }
        /// <summary>
        /// In case of a warning, the returned string of symbols begins with ‘warning:’ (reserved for future use).
        /// </summary>
        public string? Warning { get; set; }

    }
}
