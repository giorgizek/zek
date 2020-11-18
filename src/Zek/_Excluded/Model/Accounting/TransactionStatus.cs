using System.ComponentModel.DataAnnotations;
//using Zek.Localization;

namespace Zek.Model.Accounting
{
    public enum TransactionStatus
    {
        None = 0,
        /// <summary>
        /// successfully completed transaction
        /// </summary>
        //[Display(Name = nameof(TransactionStatusResources.Ok), ResourceType = typeof(TransactionStatusResources))]
        Ok = 1,
        /// <summary>
        /// transaction has failed
        /// </summary>
        //[Display(Name = nameof(TransactionStatusResources.Failed), ResourceType = typeof(TransactionStatusResources))]
        Failed = 2,
        /*
        /// <summary>
        /// transaction just registered in the system
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.Created), ResourceType = typeof(TransactionStatusResources))]
        Created = 3,
        /// <summary>
        /// transaction is not accomplished yet
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.Pending), ResourceType = typeof(TransactionStatusResources))]
        Pending = 4,
        /// <summary>
        /// transaction declined by ECOMM, because ECI is in blocked ECI list (ECOMM server side configuration)
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.Declined), ResourceType = typeof(TransactionStatusResources))]
        Declined = 5,
        /// <summary>
        /// transaction is reversed
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.Reversed), ResourceType = typeof(TransactionStatusResources))]
        Reversed = 6,
        /// <summary>
        /// transaction is reversed by autoreversal
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.AutoReversed), ResourceType = typeof(TransactionStatusResources))]
        AutoReversed = 7,
        /// <summary>
        /// transaction was timed out
        /// </summary>
        [Display(Name = nameof(TransactionStatusResources.Timeout), ResourceType = typeof(TransactionStatusResources))]
        Timeout = 8
        */

    }
}
