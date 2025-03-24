using System.Text.RegularExpressions;

namespace Zek.Utils
{
    public enum IbanValidationResult
    {
        IsValid,
        ValueMissing,
        ValueTooSmall,
        ValueTooBig,
        ValueFailsModule97Check,
        CountryCodeNotKnown
    }

    /// <summary>
    /// ვალიდაციის კლასი (ნომრიანი ტექსტის, ელ.ფოსტის).
    /// </summary>
    public class ValidationHelper
    {
        /// <summary>
        /// Check if string is only digits.
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool IsNumeric(string? value)
        {
            if (string.IsNullOrEmpty(value)) return false;

            foreach (var c in value)
            {
                if (!char.IsDigit(c))
                    return false;
            }

            return true;
        }

        //public static bool IsValidEmail(string email)
        //{
        //    if (string.IsNullOrWhiteSpace(email))
        //        return false;

        //    // only return true if there is only 1 '@' character
        //    // and it is neither the first nor the last character
        //    var found = false;
        //    for (var i = 0; i < email.Length; i++)
        //    {
        //        if (email[i] == '@')
        //        {
        //            if (found || i == 0 || i == email.Length - 1)
        //            {
        //                return false;
        //            }
        //            found = true;
        //        }
        //    }

        //    return found;
        //}

        private const string _emailExpression = @"^((([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+(\.([a-z]|\d|[!#\$%&'\*\+\-\/=\?\^_`{\|}~]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+)*)|((\x22)((((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(([\x01-\x08\x0b\x0c\x0e-\x1f\x7f]|\x21|[\x23-\x5b]|[\x5d-\x7e]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(\\([\x01-\x09\x0b\x0c\x0d-\x7f]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF]))))*(((\x20|\x09)*(\x0d\x0a))?(\x20|\x09)+)?(\x22)))@((([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])|(([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])([a-z]|\d|-||_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])*([a-z]|\d|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))\.)+(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+|(([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])+([a-z]+|\d|-|\.{0,1}|_|~|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])?([a-z]|[\u00A0-\uD7FF\uF900-\uFDCF\uFDF0-\uFFEF])))$";
        private static Regex CreateEmailRegEx()
        {
            // Workaround for CVE-2015-2526
            // If no REGEX_DEFAULT_MATCH_TIMEOUT is specified in the AppDomain, default to 2 seconds. 
            // if we're on Netstandard 1.0 we don't have access to AppDomain, so just always use 2 second timeout there. 

#if NETSTANDARD1_1 || NETSTANDARD1_6
			return new Regex(_expression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
#else
            try
            {
                if (AppDomain.CurrentDomain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT") == null)
                {
                    return new Regex(_emailExpression, RegexOptions.IgnoreCase, TimeSpan.FromSeconds(2.0));
                }
            }
            catch
            {
            }

            return new Regex(_emailExpression, RegexOptions.IgnoreCase);
#endif
        }

        public static bool IsValidEmail(string? email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            Regex regex = CreateEmailRegEx();

            if (!regex.IsMatch(email))
            {
                return false;
            }

            return true;
        }




        /// <summary>
        /// Validate if user charactes and length is valid
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="minLength"></param>
        /// <param name="maxLength"></param>
        /// <param name="allowedUserNameCharacters"></param>
        /// <returns></returns>
        public static bool IsValidUserName(string? userName, int minLength = 3, int maxLength = 256, string allowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+")
        {
            if (userName == null || userName.Length < minLength || userName.Length > maxLength)
                return false;

            if (!string.IsNullOrEmpty(allowedUserNameCharacters) && userName.Any(c => !allowedUserNameCharacters.Contains(c)))
                return false;

            return true;
        }




        #region Phone

        private const string AdditionalPhoneNumberCharacters = "-.()";
        private const string ExtensionAbbreviationExtDot = "ext.";
        private const string ExtensionAbbreviationExt = "ext";
        private const string ExtensionAbbreviationX = "x";


        public static bool IsValidPhone(string? phoneNumber)
        {
            if (string.IsNullOrEmpty(phoneNumber))
                return false;


            phoneNumber = phoneNumber.Replace("+", string.Empty).TrimEnd();
            phoneNumber = RemoveExtension(phoneNumber);

            var digitFound = false;
            foreach (var c in phoneNumber)
            {
                if (char.IsDigit(c))
                {
                    digitFound = true;
                    break;
                }
            }

            if (!digitFound)
            {
                return false;
            }

            foreach (var c in phoneNumber)
            {
                if (!(char.IsDigit(c)
                    || char.IsWhiteSpace(c)
                    || AdditionalPhoneNumberCharacters.IndexOf(c) != -1))
                {
                    return false;
                }
            }

            return true;
        }

        private static string RemoveExtension(string potentialPhoneNumber)
        {
            var lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationExtDot, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationExtDot.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationExt, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationExt.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            lastIndexOfExtension = potentialPhoneNumber.LastIndexOf(ExtensionAbbreviationX, StringComparison.OrdinalIgnoreCase);
            if (lastIndexOfExtension >= 0)
            {
                var extension = potentialPhoneNumber.Substring(lastIndexOfExtension + ExtensionAbbreviationX.Length);
                if (MatchesExtension(extension))
                {
                    return potentialPhoneNumber.Substring(0, lastIndexOfExtension);
                }
            }

            return potentialPhoneNumber;
        }

        private static bool MatchesExtension(string potentialExtension)
        {
            potentialExtension = potentialExtension.TrimStart();
            if (potentialExtension.Length == 0)
            {
                return false;
            }

            foreach (var c in potentialExtension)
            {
                if (!char.IsDigit(c))
                {
                    return false;
                }
            }

            return true;
        }

        #endregion


        //private const string _urlPattern = @"^(http|https)://([\w-]+(\.[\w-]+)+)(:\d+)?(/[\w- ./?%&=]*)?$";

        public static bool IsValidUrl(string url)
        {
            // Use Regex.IsMatch to validate the URL
            //return Regex.IsMatch(url, _urlPattern, RegexOptions.IgnoreCase);
            return Uri.TryCreate(url, UriKind.Absolute, out var uriResult)
                   && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
        }
    }
}
