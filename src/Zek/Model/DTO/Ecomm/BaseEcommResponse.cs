namespace Zek.Model.DTO.Ecomm
{
    public class BaseEcommResponse
    {
        /// <summary>
        /// In case of an error, the returned string of symbols begins with ‘error:‘
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Response from web request.
        /// </summary>

        public string Response { get; set; }
    }
}