namespace Zek.Model.DTO.Ecomm
{
    /// <summary>
    /// transaction result, Payment Server interpretation (shown only if configured to return ECOMM2 specific details – see ecomm.server.version parameter in 2.2.3 chapter)
    /// </summary>
    public enum EcommResultPaymentServer
    {
        None = 0,
        /// <summary>
        /// successfully completed payment
        /// </summary>
        Finished,
        /// <summary>
        /// cancelled payment
        /// </summary>
        Cancelled,
        /// <summary>
        /// returned payment
        /// </summary>
        Returned,
        /// <summary>
        /// registered and not yet completed payment
        /// </summary>
        Active,
    }
}