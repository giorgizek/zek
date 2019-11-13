namespace Zek.Model.DTO.Ecomm
{
    public enum EcommSecure3D
    {
        None = 0,
        /// <summary>
        ///  successful 3D Secure authorization
        /// </summary>
        Authenticated,
        /// <summary>
        /// failed 3D Secure authorization
        /// </summary>
        Declined,
        /// <summary>
        /// cardholder is not a member of 3D Secure scheme
        /// </summary>
        Notparticipated,
        /// <summary>
        /// card is not in 3D secure card range defined by issuer
        /// </summary>
        NoRange,
        /// <summary>
        /// cardholder 3D secure authorization using attempts ACS server
        /// </summary>
        Attempted,
        /// <summary>
        /// cardholder 3D secure authorization is unavailable
        /// </summary>
        Unavailable,
        /// <summary>
        /// error message received from ACS server
        /// </summary>
        Error,
        /// <summary>
        /// 3D secure authorization ended with system error
        /// </summary>
        Syserror,
        /// <summary>
        /// 3D secure authorization was attempted by wrong card scheme (Dinners club, American Express)
        /// </summary>
        Unknownscheme
    }
}