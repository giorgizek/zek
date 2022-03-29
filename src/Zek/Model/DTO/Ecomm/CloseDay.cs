﻿namespace Zek.Model.DTO.Ecomm
{
    public class CloseDay
    {
        public bool Success { get; private set; }

        public string ResultText { get; set; }

        private EcommResult result;
        public EcommResult Result
        {
            get => result;
            set
            {
                if (value != result)
                {
                    result = value;
                    Success = value == EcommResult.Ok;
                }
            }
        }


        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string ResultCode { get; set; }

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
