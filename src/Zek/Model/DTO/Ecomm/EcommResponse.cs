namespace Zek.Model.DTO.Ecomm
{
    internal class EcommResponse
    {
        /// <summary>
        /// transaction identifier (28 characters in base64 encoding)
        /// </summary>
        public string TransactionId { get; set; }

        /// <summary>
        /// In case of an error, the returned string of symbols begins with ‘error:‘
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// In case of a warning, the returned string of symbols begins with ‘warning:’ (reserved for future use).
        /// </summary>
        public string Warning { get; set; }


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
                    switch (_resultText)
                    {
                        case "OK":
                            Result = EcommResult.Ok;
                            break;
                        case "FAILED":
                            Result = EcommResult.Failed;
                            break;
                        case "CREATED":
                            Result = EcommResult.Created;
                            break;
                        case "PENDING":
                            Result = EcommResult.Pending;
                            break;
                        case "DECLINED":
                            Result = EcommResult.Declined;
                            break;
                        case "REVERSED":
                            Result = EcommResult.Reversed;
                            break;
                        case "AUTOREVERSED":
                            Result = EcommResult.AutoReversed;
                            break;
                        case "TIMEOUT":
                            Result = EcommResult.Timeout;
                            break;

                        default:
                            Result = EcommResult.None;
                            break;
                    }
                }

            }
        }
        public EcommResult Result { get; private set; }

        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string ResultCode { get; set; }

        private string _resultPaymentServerText;


        /// <summary>
        /// transaction result, Payment Server interpretation (shown only if configured to return ECOMM2 specific details – see ecomm.server.version parameter in 2.2.3 chapter):
        /// FINISHED – successfully completed payment,
        /// CANCELLED – cancelled payment,
        /// RETURNED – returned payment,
        /// ACTIVE – registered and not yet completed payment.
        /// </summary>
        public string ResultPaymentServerText
        {
            get => _resultPaymentServerText;
            set
            {
                if (value != _resultPaymentServerText)
                {
                    _resultPaymentServerText = value;
                    switch (_resultText)
                    {
                        case "FINISHED":
                            ResultPaymentServer = EcommResultPaymentServer.Finished;
                            break;
                        case "CANCELLED":
                            ResultPaymentServer = EcommResultPaymentServer.Cancelled;
                            break;
                        case "RETURNED":
                            ResultPaymentServer = EcommResultPaymentServer.Returned;
                            break;
                        case "ACTIVE":
                            ResultPaymentServer = EcommResultPaymentServer.Active;
                            break;

                        default:
                            ResultPaymentServer = EcommResultPaymentServer.None;
                            break;
                    }
                }
            }
        }
        public EcommResultPaymentServer ResultPaymentServer { get; set; }

        private string _secure3DText;
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
        public string Secure3DText
        {
            get => _secure3DText;
            set
            {
                if (value != _secure3DText)
                {
                    _secure3DText = value;
                    switch (_secure3DText)
                    {
                        case "AUTHENTICATED":
                            Secure3D = EcommSecure3D.Authenticated;
                            break;
                        case "DECLINED":
                            Secure3D = EcommSecure3D.Declined;
                            break;
                        case "NOTPARTICIPATED":
                            Secure3D = EcommSecure3D.Notparticipated;
                            break;
                        case "NO_RANGE":
                            Secure3D = EcommSecure3D.NoRange;
                            break;
                        case "ATTEMPTED":
                            Secure3D = EcommSecure3D.Attempted;
                            break;
                        case "UNAVAILABLE":
                            Secure3D = EcommSecure3D.Unavailable;
                            break;
                        case "ERROR":
                            Secure3D = EcommSecure3D.Error;
                            break;
                        case "SYSERROR":
                            Secure3D = EcommSecure3D.Syserror;
                            break;
                        case "UNKNOWNSCHEME":
                            Secure3D = EcommSecure3D.Unknownscheme;
                            break;

                        default:
                            Secure3D = EcommSecure3D.None;
                            break;
                    }
                }
            }
        }
        public EcommSecure3D Secure3D { get; private set; }

        /// <summary>
        /// Reoccurring payment (if available) identification in Payment Server.
        /// </summary>
        public string RegularPaymentId { get; set; }

        /// <summary>
        /// Reoccurring payment (if available) expiry date in Payment Server in form of YYMM
        /// </summary>
        public string RegularPaymentExpiry { get; set; }

        /// <summary>
        /// Merchant Transaction Identifier (if available) for Payment – shown if it was sent as additional parameter with name “mrch_transaction_id” on Payment registration.
        /// </summary>
        public string MerchantTransactionId { get; set; }



        /// <summary>
        /// the results of the verification of hash value in AAV merchant name (only if failed):
        /// FAILED – hash value fails to match
        /// </summary>
        public string Aav { get; set; }




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
        /// refund transaction identifier – applicable for obtaining refund payment details or to request refund payment reversal.
        /// </summary>
        public string RefundTransactionId { get; set; }

        /// <summary>
        /// the number of credit reversals (up to 10 digits), shown only if result_code begins with 5
        /// </summary>
        public string Fld075 { get; set; }
        public int CreditReversalCount { get; set; }


        /// <summary>
        /// the number of debit transactions (up to 10 digits), shown only if result_code begins with 5
        /// </summary>
        public string Fld076 { get; set; }
        public int DebitTransactionCount { get; set; }

        /// <summary>
        /// total amount of credit reversals (up to 16 digits), shown only if result_code begins with 5
        /// </summary>
        public string Fld087 { get; set; }
        public long CreditReversalTotal { get; set; }

        /// <summary>
        /// total amount of debit transactions (up to 16 digits), shown only if result_code begins with 5
        /// </summary>
        public string Fld088 { get; set; }
        public long DebitTransactionTotal { get; set; }
    }
}