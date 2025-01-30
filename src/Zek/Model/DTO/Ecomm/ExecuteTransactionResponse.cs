namespace Zek.Model.DTO.Ecomm
{
    public class ExecuteTransactionResponse : BaseEcommResponse
    {
        private string? _resultText;
        /// <summary>
        /// transaction results: OK – successful transaction, FAILED – failed transaction
        /// </summary>
        public string? ResultText
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

        public EcommResult Result { get; set; }

        public bool Success { get; private set; }

        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// 108 – Merchant communication with cardholder has to be done;
        /// 114 – It is possible to try to execute the transaction next time;
        /// 180 – Cardholder ended cooperation.Regular payment has been deleted;
        /// 2xx – Regular payment has been deleted.
        /// </summary>
        public string? ResultCode { get; set; }


        /// <summary>
        /// retrieval reference number returned from Card Suite Processing RTPS (12 characters)
        /// </summary>
        public string? Rrn { get; set; }

        /// <summary>
        /// approval code returned from Card Suite Processing RTPS (max 6 characters) 
        /// </summary>
        public string? ApprovalCode { get; set; }
    }
}