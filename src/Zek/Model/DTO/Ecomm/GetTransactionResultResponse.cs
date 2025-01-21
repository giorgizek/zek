using System.Globalization;

namespace Zek.Model.DTO.Ecomm
{
    public class GetTransactionResultResponse
    {
        public bool Success { get; private set; }

        public EcommResult Result { get; set; }
        /// <summary>
        /// transaction results: OK – successful transaction, FAILED – failed transaction
        /// </summary>
        public string ResultText { get; set; }

        private string _resultCode;
        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string ResultCode
        {
            get => _resultCode;
            set
            {
                if (value == _resultCode) return;
                _resultCode = value;
                Success = value == "000";
            }
        }

        public EcommResultPaymentServer ResultPaymentServer { get; set; }
        /// <summary>
        /// transaction result, Payment Server interpretation (shown only if configured to return ECOMM2 specific details – see ecomm.server.version parameter in 2.2.3 chapter):
        /// FINISHED – successfully completed payment,
        /// CANCELLED – cancelled payment,
        /// RETURNED – returned payment,
        /// ACTIVE – registered and not yet completed payment.
        /// </summary>
        public string ResultPaymentServerText { get; set; }

        public EcommSecure3D Secure3D { get; set; }
        /// <summary>
        /// 3D Secure status:
        /// AUTHENTICATED – successful 3D Secure authorization
        /// DECLINED – failed 3D Secure authorization
        /// NOTPARTICIPATED – cardholder is not a member of 3D Secure scheme
        /// NO_RANGE – card is not in 3D secure card range defined by issuer
        /// ATTEMPTED – cardholder 3D secure authorization using attempts ACS server
        /// UNAVAILABLE – cardholder 3D secure authorization is unavailable
        /// ERROR – error message received from ACS server
        /// SYSERROR – 3D secure authorization ended with system error
        /// UNKNOWNSCHEME – 3D secure authorization was attempted by wrong card scheme (Dinners club, American Express)
        /// </summary>
        public string Secure3DText { get; set; }

        /// <summary>
        /// retrieval reference number returned from Card Suite Processing RTPS (12 characters)
        /// </summary>
        public string Rrn { get; set; }

        /// <summary>
        /// approval code returned from Card Suite Processing RTPS (max 6 characters) 
        /// </summary>
        public string ApprovalCode { get; set; }

        /// <summary>
        /// masked card number
        /// </summary>
        public string CardNumber { get; set; }

        /// <summary>
        /// the results of the verification of hash value in AAV merchant name (only if failed):
        /// FAILED – hash value fails to match
        /// </summary>
        public string Aav { get; set; }

        /// <summary>
        /// Reoccurring payment (if available) identification in Payment Server.
        /// </summary>
        public string RegularPaymentId { get; set; }

        private string _regularPaymentExpiryText;

        /// <summary>
        /// Reoccurring payment (if available) expiry date in Payment Server in form of YYMM
        /// </summary>
        public string RegularPaymentExpiryText
        {
            get => _regularPaymentExpiryText;
            set
            {
                if (value != _regularPaymentExpiryText)
                {
                    _regularPaymentExpiryText = value;

                    RegularPaymentExpiry = DateTime.TryParseExact(_regularPaymentExpiryText, "MMyy", null, DateTimeStyles.None, out var date)
                        ? date
                        : (DateTime?)null;
                }
            }
        }

        public DateTime? RegularPaymentExpiry { get; set; }

        /// <summary>
        /// Merchant Transaction Identifier (if available) for Payment – shown if it was sent as additional parameter with name “mrch_transaction_id” on Payment registration.
        /// </summary>
        public string MerchantTransactionId { get; set; }

        public string Error { get; set; }
        public string Warning { get; set; }
        public string Response { get; set; }
    }
}
