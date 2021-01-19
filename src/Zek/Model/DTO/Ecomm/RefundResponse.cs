namespace Zek.Model.DTO.Ecomm
{
    public class RefundResponse : BaseEcommResponse
    {
        public EcommResult Result { get; set; }

        private string _resultText;
        /// <summary>
        /// transaction results: OK – successful transaction, FAILED – failed transaction
        /// </summary>
        public string ResultText
        {
            get => _resultText;
            set
            {
                if (value != _resultText)
                {
                    _resultText = value;
                    Success = _resultText == "OK";
                }
            }
        }

        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string ResultCode { get; set; }

        public bool Success { get; private set; }

        /// <summary>
        /// refund transaction identifier – applicable for obtaining refund payment details or to request refund payment reversal.
        /// </summary>
        public string RefundTransactionId { get; set; }

        /// <summary>
        /// reserved for future use.
        /// </summary>
        public string Warning { get; set; }
    }
}
