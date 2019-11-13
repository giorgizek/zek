namespace Zek.Model.DTO.Ecomm
{
    public class ReverseResponseDTO : BaseResponseDTO
    {
        public EcommResult Result { get; set; }
        /// <summary>
        /// transaction results: OK – successful reversal transaction, REVERSED – transaction has already been reversed, FAILED – failed to reverse transaction(transaction status remains as it was)
        /// </summary>
        public string ResultText { get; set; }

        /// <summary>
        /// transaction result code returned from Card Suite Processing RTPS (3 digits)
        /// </summary>
        public string ResultCode { get; set; }

        /// <summary>
        /// reserved for future use.
        /// </summary>
        public string Warning { get; set; }
    }
}
