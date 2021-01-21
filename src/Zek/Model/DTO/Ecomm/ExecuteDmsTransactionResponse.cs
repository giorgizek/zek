namespace Zek.Model.DTO.Ecomm
{
    public class ExecuteDmsTransactionResponse : ExecuteTransactionResponse
    {
        /// <summary>
        /// Masked card number
        /// </summary>
        public string CardNumber { get; set; }

    }
}