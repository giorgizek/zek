namespace Zek.Model.DTO.Ecomm
{
    public class ExecuteDmsTransactionResponse : ExecuteTransactionResponse
    {
        /// <summary>
        /// masked card number
        /// </summary>
        public string CardNumber { get; set; }

    }
}