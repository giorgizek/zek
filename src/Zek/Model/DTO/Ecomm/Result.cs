namespace Zek.Model.DTO.Ecomm
{
    public enum EcommResult
    {
        None = 0,
        /// <summary>
        /// successfully completed transaction
        /// </summary>
        Ok = 1,
        /// <summary>
        /// transaction has failed
        /// </summary>
        Failed =2 ,
        /// <summary>
        /// transaction just registered in the system
        /// </summary>
        Created = 3,
        /// <summary>
        /// transaction is not accomplished yet
        /// </summary>
        Pending = 4,
        /// <summary>
        /// transaction declined by ECOMM, because ECI is in blocked ECI list (ECOMM server side configuration)
        /// </summary>
        Declined = 5,
        /// <summary>
        /// transaction is reversed
        /// </summary>
        Reversed = 6,
        /// <summary>
        /// transaction is reversed by autoreversal
        /// </summary>
        AutoReversed = 7,
        /// <summary>
        /// transaction was timed out
        /// </summary>
        Timeout = 8,
    }
}