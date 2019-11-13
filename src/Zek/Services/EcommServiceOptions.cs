namespace Zek.Services
{

    /// <summary>
    /// ECOMM system config
    /// </summary>
    public class EcommServiceOptions
    {
        /// <summary>
        /// Certificate file (e.g. D:\securepay.ufc.ge_1234567_merchant_wp.p12)
        /// </summary>
        public string CertificateFile { get; set; }

        /// <summary>
        /// Certificate file password
        /// </summary>
        public string CertificatePassword { get; set; }

        /// <summary>
        /// Server Url (e.g. "https://securepay.ufc.ge:18443/ecomm2/MerchantHandler)
        /// </summary>
        public string ServerUrl { get; set; }


        /// <summary>
        /// Client Url (Readdressed url to ECOMM payment server e.g. "https://securepay.ufc.ge:18443/ecomm2/ClientHandler)
        /// </summary>
        public string ClientUrl { get; set; }
    }
}
