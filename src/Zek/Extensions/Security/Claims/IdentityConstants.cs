namespace Zek.Extensions.Security.Claims
{
    public class IdentityConstants
    {
        private const string IdentityPrefix = "Identity";
      
        /// <summary>
        /// The scheme used to identify application authentication cookies.
        /// </summary>
        public static readonly string ApplicationScheme = IdentityPrefix + ".Application";

    }
}
