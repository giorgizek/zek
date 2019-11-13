namespace Zek.Model.DTO.Ecomm
{
    public class ExecuteDmsTransactionDTO : ExecuteTransactionDTO
    {
        /// <summary>
        /// masked card number
        /// </summary>
        public string CardNumber { get; set; }

    }
}