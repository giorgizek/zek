namespace Zek.Model.Notification
{
    public enum SmsStatus
    {
        /// <summary>
        /// Pending
        /// </summary>
        Pending = 10,
        /// <summary>
        /// Sending
        /// </summary>
        Sending = 20,
        /// <summary>
        /// Complete
        /// </summary>
        Sent = 30,
        /// <summary>
        /// Partially Sent
        /// </summary>
        PartiallySent = 35,
        /// <summary>
        /// Delivered
        /// </summary>
        Delivered = 40,
        /// <summary>
        /// Cancelled
        /// </summary>
        Cancelled = 50,
        /// <summary>
        /// Error
        /// </summary>
        Error = 60,
        /// <summary>
        /// Failed
        /// </summary>
        Failed = 70,
    }
}