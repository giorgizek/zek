namespace Zek.Model.DTO.Ecomm
{
    public class ExecuteRegularPaymentDTO : ExecuteTransactionDTO
    {
        /// <summary>
        /// transaction identifier (28 characters in base64 encoding)
        /// </summary>
        public string TransactionId { get; set; }

    }
}
